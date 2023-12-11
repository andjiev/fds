namespace FDS.Package.Service.Consumers
{
    using AutoMapper;
    using FDS.Common.Messages.Commands;
    using FDS.Package.Domain.Repositories;
    using FDS.Package.Service.Hubs;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;

    public class ImportPackagesCompletedConsumer : IConsumer<IImportPackagesCompleted>
    {
        private readonly IPackageRepository repository;
        private readonly IHubContext<PackageHub> hub;
        private readonly IMapper mapper;

        public ImportPackagesCompletedConsumer(IPackageRepository repository, IHubContext<PackageHub> hub, IMapper mapper)
        {
            this.repository = repository;
            this.hub = hub;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IImportPackagesCompleted> context)
        {
            try
            {
                var packages = await repository.GetAsync();
                await hub.Clients.All.SendAsync("importCompleted", mapper.Map<List<Models.Package>>(packages));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while sendig message" + ex);
            }
        }
    }
}
