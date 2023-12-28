namespace FDS.Package.Service.Commands.Handlers
{
    using AutoMapper;
    using FDS.Common.Messages;
    using FDS.Common.Messages.Commands;
    using MassTransit;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using FDS.Common.Extensions;
    using System;
    using FDS.Package.Domain.Repositories;
    using Enums = FDS.Common.DataContext.Enums;
    using FDS.Package.Service.Hubs;
    using Microsoft.AspNetCore.SignalR;

    public class ImportPackagesCommandHandler : IRequestHandler<ImportPackagesCommand, Unit>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;
        private readonly ISettingsRepository repository;
        private readonly IHubContext<PackageHub> hub;


        public ImportPackagesCommandHandler(IBus bus, IRabbitMQConfiguration configuration, ISettingsRepository repository, IHubContext<PackageHub> hub)
        {
            this.bus = bus;
            this.configuration = configuration;
            this.repository = repository;
            this.hub = hub;
        }

        public async Task<Unit> Handle(ImportPackagesCommand request, CancellationToken cancellationToken)
        {
            await repository.UpdateImportState(Enums.ImportState.Importing);
            await ImportPackages(cancellationToken);
            await hub.Clients.All.SendAsync("importStarted");
            return Unit.Task.Result;
        }

        private async Task ImportPackages(CancellationToken cancellationToken)
        {
            var correlation = Guid.NewGuid().ToString("N");
            var endpoint = await bus
                .GetSendEndpoint(configuration.GetEndpointUrl(bus.Address, "ImportPackages"))
                .ConfigureAwait(false);

            await endpoint.Send<IImportPackages>(new
            {
                CorrelationId = correlation
            },
            cancellationToken: cancellationToken);
        }
    }
}
