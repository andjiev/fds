namespace FDS.Update.Service.Consumers
{
    using FDS.Common.Messages.Commands;
    using FDS.Update.Domain.Repositories;
    using MassTransit;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;
    using Entities = FDS.Common.Entities;
    using AutoMapper;
    using System.Xml.Linq;
    using System.Linq;
    using FDS.Common.DataContext.Enums;
    using System.Diagnostics;

    public class InstallPackageConsumer : IConsumer<IInstallPackage>
    {
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public InstallPackageConsumer(IPackageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IInstallPackage> context)
        {
            try
            {
                var process = new Process();
                process.StartInfo.WorkingDirectory = "../../../../";
                process.StartInfo.FileName = "/usr/local/bin/npm";
                process.StartInfo.Arguments = "install " + context.Message.Name + "@latest";

                if (context.Message.Type == PackageType.Dev)
                {
                    process.StartInfo.Arguments += " --save-dev";
                }

                process.Start();
                await process.WaitForExitAsync();

                var package = await CreatePackage(context.Message.Name, context.Message.Version, context.Message.Description, context.Message.Type);
                var packageId = await repository.InsertPackageAsync(mapper.Map<Entities.Package>(package));
                await context.Publish<IPackageInstalled>(new
                {
                    context.Message.CorrelationId,
                    PackageId = packageId
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while installing package");
            }
        }

        private async Task<Models.Package> CreatePackage(string packageName, string packageVersion, string description, PackageType type)
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            var httpClient = new HttpClient(clientHandler);
            string snykUrl = "https://snyk.io/advisor/npm-package/" + packageName;
            string scoreUrl = snykUrl + "/badge.svg";
            HttpResponseMessage scoreResponse = await httpClient.GetAsync(scoreUrl);
            XDocument xDoc = XDocument.Load(scoreResponse.Content.ReadAsStreamAsync().Result);
            var svgElement = xDoc.Root;
            var scoreTitle = svgElement.Descendants().Where(x => x.Name.LocalName == "title").FirstOrDefault();
            var score = scoreTitle?.Value.Split(" ")[2];
            var scoreValue = score?.Split("/")[0];

            return new Models.Package
            {
                Name = packageName,
                CurrentVersion = packageVersion,
                LatestVersion = packageVersion,
                Score = !string.IsNullOrEmpty(scoreValue) ? Convert.ToInt32(scoreValue) : null,
                Url = snykUrl,
                Description = description,
                Status = PackageStatus.UpToDate,
                Type = type
            };
        }
    }
}
