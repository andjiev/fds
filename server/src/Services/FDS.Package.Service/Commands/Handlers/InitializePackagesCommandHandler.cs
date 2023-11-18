namespace FDS.Package.Service.Commands.Handlers
{
    using AutoMapper;
    using FDS.Package.Domain.Repositories;
    using FDS.Package.Service.Hubs;
    using MediatR;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Enums = Common.DataContext.Enums;

    public class InitializePackagesCommandHandler : IRequestHandler<InitializePackagesCommand, Unit>
    {
        private readonly IPackageRepository packageRepository;
        private readonly IHubContext<PackageHub> hub;
        private readonly IMapper mapper;

        public InitializePackagesCommandHandler(IPackageRepository packageRepository, IHubContext<PackageHub>  hub, IMapper mapper)
        {
            this.packageRepository = packageRepository;
            this.hub = hub;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(InitializePackagesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using FileStream stream = File.OpenRead("../../../package.json");
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

                await packageRepository.InsertPackagesAsync(mapper.Map<List<Domain.Entities.Package>>(packages));

                await hub.Clients.All.SendAsync("syncPackages", packages);
                return Unit.Task.Result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
