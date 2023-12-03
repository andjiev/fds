namespace FDS.Package.Service.Consumers
{
    using FDS.Common.Messages.Commands;
    using FDS.Package.Service.Hubs;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Threading.Tasks;

    public class PackageDeletedConsumer : IConsumer<IPackageDeleted>
    {
        private readonly IHubContext<PackageHub> hub;

        public PackageDeletedConsumer(IHubContext<PackageHub> hub)
        {
            this.hub = hub;
        }

        public async Task Consume(ConsumeContext<IPackageDeleted> context)
        {
            try
            {
                await hub.Clients.All.SendAsync("packageDeleted", context.Message.PackageId, context.Message.PackageName);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while sendig message");
            }
        }
    }
}
