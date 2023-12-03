namespace FDS.Package.Service.Commands.Handlers
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
    using Models = FDS.Common.Models;

    public class UpdateSelectedPackagesCommandHandler : IRequestHandler<UpdateSelectedPackagesCommand, List<Models.Package>>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public UpdateSelectedPackagesCommandHandler(IBus bus, IRabbitMQConfiguration configuration, IPackageRepository repository, IMapper mapper)
        {
            this.bus = bus;
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<Models.Package>> Handle(UpdateSelectedPackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = await repository.GetAsync(request.PackageIds);
            var packagesToReturn = new List<Models.Package>();

            foreach(var package in packages)
            {
                if(package.Status == PackageStatus.UpdateNeeded)
                {
                    package.UpdateStatus(PackageStatus.Loading);
                    await repository.UpdatePackageAsync(package);
                    await StartPackageUpdate(package.Id, package.Name, package.LatestVersion, cancellationToken);
                }
                packagesToReturn.Add(mapper.Map<Models.Package>(package));
            }

            return packagesToReturn;
        }

        private async Task StartPackageUpdate(int packageId, string packageName, string packageVersion, CancellationToken cancellationToken)
        {
            var correlation = Guid.NewGuid().ToString("N");
            var endpoint = await bus
                .GetSendEndpoint(configuration.GetEndpointUrl(bus.Address, "StartUpdate"))
                .ConfigureAwait(false);

            await endpoint.Send<IStartUpdate>(new
            {
                CorrelationId = correlation,
                PackageId = packageId,
                PackageName = packageName,
                PackageVersion = packageVersion
            }, cancellationToken: cancellationToken);
        }
    }
}
