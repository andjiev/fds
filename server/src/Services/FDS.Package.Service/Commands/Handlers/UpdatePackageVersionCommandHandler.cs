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
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdatePackageVersionCommandHandler : IRequestHandler<UpdatePackageVersionCommand, Models.Package>
    {
        private readonly IBus bus;
        private readonly IRabbitMQConfiguration configuration;
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public UpdatePackageVersionCommandHandler(IBus bus, IRabbitMQConfiguration configuration, IPackageRepository repository, IMapper mapper)
        {
            this.bus = bus;
            this.configuration = configuration;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Models.Package> Handle(UpdatePackageVersionCommand request, CancellationToken cancellationToken)
        {
            var package = await repository.GetPackageAsync(request.PackageId, false);
            if (package == null)
            {
                throw new ArgumentNullException("Package id is not valid");
            }

            package.UpdateStatus(PackageStatus.Updating);

            await repository.UpdatePackageAsync(package);
            await StartPackageUpdate(request.PackageId, request.VersionId, cancellationToken);
            return mapper.Map<Models.Package>(package);
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
