namespace FDS.Package.Service.Commands.Handlers
{
    using FDS.Common.DataContext.Enums;
    using FDS.Common.Extensions;
    using FDS.Common.Messages;
    using FDS.Common.Messages.Commands;
    using MassTransit;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, Unit>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;

        public CreatePackageCommandHandler(IBus bus, IRabbitMQConfiguration configuration)
        {
            this.bus = bus;
            this.configuration = configuration;
        }

        public async Task<Unit> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            await InstallPackage(request.PackageName, request.PackageDescription, request.PackageVersion, request.PackageType, cancellationToken);
            return Unit.Task.Result;
        }

        private async Task InstallPackage(string packageName, string packageDescription, string packageVersion, PackageType packageType, CancellationToken cancellationToken)
        {
            var correlation = Guid.NewGuid().ToString("N");
            var endpoint = await bus
                .GetSendEndpoint(configuration.GetEndpointUrl(bus.Address, "InstallPackage"))
                .ConfigureAwait(false);

            await endpoint.Send<IInstallPackage>(new
            {
                CorrelationId = correlation,
                Name = packageName,
                Description = packageDescription,
                Version = packageVersion,
                Type = packageType
            },
            cancellationToken: cancellationToken);
        }
    }
}
