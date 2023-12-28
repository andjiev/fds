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

    public class DeleteSelectedPackagesCommandHandler : IRequestHandler<DeleteSelectedPackagesCommand, Unit>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;
        private readonly IHubContext<PackageHub> hub;

        public DeleteSelectedPackagesCommandHandler(IBus bus, IRabbitMQConfiguration configuration, IPackageRepository repository, IMapper mapper, IHubContext<PackageHub> hub)
        {
            this.bus = bus;
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
            this.hub = hub;
        }

        public async Task<Unit> Handle(DeleteSelectedPackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = await repository.GetAsync(request.PackageIds);
            var packagesToReturn = new List<Models.Package>();

            foreach (var package in packages)
            {
                package.UpdateStatus(PackageStatus.Loading);
                await repository.UpdatePackageAsync(package);
                await StartPackageDelete(package.Id, package.Name, cancellationToken);
                packagesToReturn.Add(mapper.Map<Models.Package>(package));
            }

            await hub.Clients.All.SendAsync("packagesModified", packagesToReturn.Select(x => x.Id));
            return Unit.Task.Result;
        }

        private async Task StartPackageDelete(int packageId, string packageName, CancellationToken cancellationToken)
        {
            var correlation = Guid.NewGuid().ToString("N");
            var endpoint = await bus
                .GetSendEndpoint(configuration.GetEndpointUrl(bus.Address, "StartDelete"))
                .ConfigureAwait(false);

            await endpoint.Send<IStartDelete>(new
            {
                CorrelationId = correlation,
                PackageId = packageId,
                PackageName = packageName,
            }, cancellationToken: cancellationToken);
        }
    }
}
