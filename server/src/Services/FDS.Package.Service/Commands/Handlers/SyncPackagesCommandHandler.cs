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

    public class SyncPackagesCommandHandler : IRequestHandler<SyncPackagesCommand, Unit>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;

        public SyncPackagesCommandHandler(IBus bus, IRabbitMQConfiguration configuration)
        {
            this.bus = bus;
            this.configuration = configuration;
        }

        public async Task<Unit> Handle(SyncPackagesCommand request, CancellationToken cancellationToken)
        {
            await SyncPackages(cancellationToken);
            return Unit.Task.Result;
        }

        private async Task SyncPackages(CancellationToken cancellationToken)
        {
            var correlation = Guid.NewGuid().ToString("N");
            var endpoint = await bus
                .GetSendEndpoint(configuration.GetEndpointUrl(bus.Address, "SyncPackages"))
                .ConfigureAwait(false);

            await endpoint.Send<ISyncPackages>(new
            {
                CorrelationId = correlation
            },
            cancellationToken: cancellationToken);
        }
    }
}
