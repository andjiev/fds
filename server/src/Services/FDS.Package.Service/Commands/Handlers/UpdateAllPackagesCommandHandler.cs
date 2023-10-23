﻿namespace FDS.Package.Service.Commands.Handlers
{
    using AutoMapper;
    using FDS.Common.DataContext.Enums;
    using FDS.Common.Extensions;
    using FDS.Common.Messages;
    using FDS.Common.Messages.Commands;
    using FDS.Package.Domain.Repositories;
    using MassTransit;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateAllPackagesCommandHandler : IRequestHandler<UpdateAllPackagesCommand, List<Models.Package>>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public UpdateAllPackagesCommandHandler(IBus bus, IRabbitMQConfiguration configuration, IPackageRepository repository, IMapper mapper)
        {
            this.bus = bus;
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<Models.Package>> Handle(UpdateAllPackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = await repository.GetAsync();
            var packagesToReturn = new List<Models.Package>();

            foreach(var package in packages)
            {
                if(package.Status == PackageStatus.Initial)
                {
                    package.UpdateStatus(PackageStatus.Updating);

                    await repository.UpdatePackageAsync(package);
                    await StartPackageUpdate(package.Id, package.VersionUpdate.Id, cancellationToken);
                    var test = mapper.Map<Models.Package>(package, opt =>
                    {
                        opt.AfterMap((src, dest) => dest.VersionUpdate = null);
                    });
                    packagesToReturn.Add(mapper.Map<Models.Package>(package, opt =>
                    {
                        opt.AfterMap((src, dest) => dest.VersionUpdate = null);
                    }));
                }
            }

            return packagesToReturn;
        }

        private async Task StartPackageUpdate(int packageId, int versionId, CancellationToken cancellationToken)
        {
            var correlation = Guid.NewGuid().ToString("N");
            var endpoint = await bus
                .GetSendEndpoint(configuration.GetEndpointUrl(bus.Address, "StartUpdate"))
                .ConfigureAwait(false);

            await endpoint.Send<IStartUpdate>(new
            {
                CorrelationId = correlation,
                PackageId = packageId,
                VersionId = versionId
            }, cancellationToken: cancellationToken);
        }
    }
}
