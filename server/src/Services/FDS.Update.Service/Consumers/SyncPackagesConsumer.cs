namespace FDS.Update.Service.Consumers
{
    using FDS.Common.Messages.Commands;
    using FDS.Update.Domain.Repositories;
    using MassTransit;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;
    using Enums = Common.DataContext.Enums;
    using Entities = FDS.Common.Entities;
    using AutoMapper;

    public class SyncPackagesConsumer : IConsumer<ISyncPackages>
    {
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public SyncPackagesConsumer(IPackageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ISyncPackages> context)
        {
            try
            {

                using FileStream stream = File.OpenRead("../../../../package.json");
                Models.PackageJson packageJson = await JsonSerializer.DeserializeAsync<Models.PackageJson>(stream);
                var httpClient = new HttpClient();
                var packages = new List<Models.Package>();

                foreach (var dependency in packageJson.Dependencies)
                {
                    string url = "https://registry.npmjs.org/" + dependency.Key + "/latest";
                    var response = await httpClient.GetAsync(url);
                    Models.VersionJson latestVersionJson = await JsonSerializer.DeserializeAsync<Models.VersionJson>(response.Content.ReadAsStreamAsync().Result);
                    string currentVersion = dependency.Value.Replace("^", "");
                    string latestVersion = latestVersionJson.Version;

                    var package = new Models.Package
                    {
                        Name = dependency.Key,
                        CurrentVersion = currentVersion,
                        LatestVersion = latestVersion,
                        Status = currentVersion == latestVersion ? Enums.PackageStatus.UpToDate : Enums.PackageStatus.UpdateNeeded
                    };
                    packages.Add(package);
                }

                await repository.InsertPackagesAsync(mapper.Map<List<Entities.Package>>(packages));
                await context.Publish<ISyncPackagesCompleted>(new
                {
                    context.Message.CorrelationId
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while synchronizing packages");
            }
        }
    }
}
