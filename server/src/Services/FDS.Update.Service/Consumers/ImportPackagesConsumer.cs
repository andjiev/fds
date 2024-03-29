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
    using System.Xml.Linq;
    using System.Linq;
    using FDS.Common.DataContext.Enums;

    public class ImportPackagesConsumer : IConsumer<IImportPackages>
    {
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public ImportPackagesConsumer(IPackageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IImportPackages> context)
        {
            try
            {
                using FileStream stream = File.OpenRead("../appdata/package.json");
                Models.PackageJson packageJson = await JsonSerializer.DeserializeAsync<Models.PackageJson>(stream);
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                var httpClient = new HttpClient(clientHandler);
                var packages = new List<Models.Package>();

                // dependencies
                foreach (var dependency in packageJson.Dependencies)
                {
                    var package = await CreatePackage(httpClient, dependency.Key, dependency.Value, PackageType.Prod);
                    packages.Add(package);
                }

                // devDependencies
                foreach (var dependency in packageJson.DevDependencies)
                {
                    var package = await CreatePackage(httpClient, dependency.Key, dependency.Value, PackageType.Dev);
                    packages.Add(package);
                }

                await repository.InsertPackagesAsync(mapper.Map<List<Entities.Package>>(packages));
                await context.Publish<IImportPackagesCompleted>(new
                {
                    context.Message.CorrelationId
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while synchronizing packages" + ex.Message);
            }
        }

        private async Task<Models.Package> CreatePackage(HttpClient httpClient, string packageName, string packageVersion, PackageType type)
        {
            string url = "https://registry.npmjs.org/" + packageName + "/latest";
            string snykUrl = "https://snyk.io/advisor/npm-package/" + packageName;
            string scoreUrl = snykUrl + "/badge.svg";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            Models.VersionJson latestVersionJson = await JsonSerializer.DeserializeAsync<Models.VersionJson>(response.Content.ReadAsStreamAsync().Result);
            string currentVersion = packageVersion.Replace("^", "");
            string latestVersion = latestVersionJson.Version;
            string description = latestVersionJson.Description;

            HttpResponseMessage scoreResponse = await httpClient.GetAsync(scoreUrl);
            XDocument xDoc = XDocument.Load(scoreResponse.Content.ReadAsStreamAsync().Result);
            var svgElement = xDoc.Root;
            var scoreTitle = svgElement.Descendants().Where(x => x.Name.LocalName == "title").FirstOrDefault();
            var score = scoreTitle?.Value.Split(" ")[2];
            var scoreValue = score?.Split("/")[0];

            return new Models.Package
            {
                Name = packageName,
                CurrentVersion = currentVersion,
                LatestVersion = latestVersion,
                Score = !string.IsNullOrEmpty(scoreValue) ? Convert.ToInt32(scoreValue) : null,
                Url = snykUrl,
                Description = description,
                Status = currentVersion == latestVersion ? PackageStatus.UpToDate : PackageStatus.UpdateNeeded,
                Type = type
            };
        }
    }
}
