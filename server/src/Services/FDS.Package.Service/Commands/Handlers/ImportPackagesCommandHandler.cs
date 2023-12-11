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

    public class ImportPackagesCommandHandler : IRequestHandler<ImportPackagesCommand, Unit>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;

        public ImportPackagesCommandHandler(IBus bus, IRabbitMQConfiguration configuration)
        {
            this.bus = bus;
            this.configuration = configuration;
        }

        public async Task<Unit> Handle(ImportPackagesCommand request, CancellationToken cancellationToken)
        {
            await ImportPackages(cancellationToken);
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
