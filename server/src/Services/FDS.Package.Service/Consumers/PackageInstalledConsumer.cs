namespace FDS.Package.Service.Consumers
{
    using AutoMapper;
    using FDS.Common.Messages.Commands;
    using FDS.Package.Domain.Repositories;
    using FDS.Package.Service.Hubs;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;

    public class PackageInstalledConsumer : IConsumer<IPackageInstalled>
    {
        private readonly IPackageRepository repository;
        private readonly IHubContext<PackageHub> hub;
        private readonly IMapper mapper;

        public PackageInstalledConsumer(IPackageRepository repository, IHubContext<PackageHub> hub, IMapper mapper)
        {
            this.repository = repository;
            this.hub = hub;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IPackageInstalled> context)
        {
            try
            {
                var package = await repository.GetPackageAsync(context.Message.PackageId);
                await hub.Clients.All.SendAsync("packageInstalled", mapper.Map<Models.Package>(package));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while sendig message");
            }
        }
    }
}
