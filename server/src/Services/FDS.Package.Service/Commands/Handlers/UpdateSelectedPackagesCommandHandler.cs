namespace FDS.Package.Service.Commands.Handlers
{
    using AutoMapper;
    using FDS.Common.DataContext.Enums;
    using FDS.Common.Extensions;
    using FDS.Common.Messages;
    using FDS.Common.Messages.Commands;
    using FDS.Package.Domain.Repositories;
    using FDS.Package.Service.Hubs;
    using MassTransit;
    using MediatR;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;

    public class UpdateSelectedPackagesCommandHandler : IRequestHandler<UpdateSelectedPackagesCommand, Unit>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;
        private readonly IHubContext<PackageHub> hub;

        public UpdateSelectedPackagesCommandHandler(IBus bus, IRabbitMQConfiguration configuration, IPackageRepository repository, IMapper mapper, IHubContext<PackageHub> hub)
        {
            this.bus = bus;
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
            this.hub = hub;
        }

        public async Task<Unit> Handle(UpdateSelectedPackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = await repository.GetAsync(request.PackageIds);
            var packagesToReturn = new List<Models.Package>();

            foreach (var package in packages)
            {
                if (package.Status == PackageStatus.UpdateNeeded)
                {
                    package.UpdateStatus(PackageStatus.Loading);
                    await repository.UpdatePackageAsync(package);
                    await StartPackageUpdate(package.Id, package.Name, package.LatestVersion, cancellationToken);
                }
                packagesToReturn.Add(mapper.Map<Models.Package>(package));
            }

            await hub.Clients.All.SendAsync("packagesModified", packagesToReturn.Select(x => x.Id));
            return Unit.Task.Result;
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
