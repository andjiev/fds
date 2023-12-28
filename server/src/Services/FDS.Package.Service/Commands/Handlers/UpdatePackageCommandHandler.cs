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
    using System.Threading;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;

    public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand, Unit>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;
        private readonly IPackageRepository repository;
        private readonly IHubContext<PackageHub> hub;

        public UpdatePackageCommandHandler(IBus bus, IRabbitMQConfiguration configuration, IPackageRepository repository, IHubContext<PackageHub> hub)
        {
            this.bus = bus;
            this.configuration = configuration;
            this.repository = repository;
            this.hub = hub;
        }

        public async Task<Unit> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
        {
            var package = await repository.GetPackageAsync(request.PackageId);
            if (package == null)
            {
                throw new ArgumentNullException("Package id is not valid");
            }

            package.UpdateStatus(PackageStatus.Loading);

            await repository.UpdatePackageAsync(package);
            await StartPackageUpdate(package.Id, package.Name, package.LatestVersion, cancellationToken);

            await hub.Clients.All.SendAsync("packagesModified", new int[] { package.Id });
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
                PackageVersion = packageVersion,
            }, cancellationToken: cancellationToken);
        }
    }
}
