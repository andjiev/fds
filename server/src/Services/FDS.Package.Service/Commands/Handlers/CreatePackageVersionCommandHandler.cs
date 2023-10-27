namespace FDS.Package.Service.Commands.Handlers
{
    using AutoMapper;
    using FDS.Package.Domain.Repositories;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreatePackageVersionCommandHandler : IRequestHandler<CreatePackageVersionCommand, Models.Package>
    {
        private readonly IPackageRepository packageRepository;
        private readonly IVersionRepository versionRepository;
        private readonly IMapper mapper;

        public CreatePackageVersionCommandHandler(IPackageRepository packageRepository, IVersionRepository versionRepository, IMapper mapper)
        {
            this.packageRepository = packageRepository;
            this.versionRepository = versionRepository;
            this.mapper = mapper;
        }

        public async Task<Models.Package> Handle(CreatePackageVersionCommand request, CancellationToken cancellationToken)
        {
            var package = await packageRepository.GetPackageAsync(request.PackageId, true, true);
            if (package == null)
            {
                throw new ArgumentNullException("Package id is not valid");
            }

            var version1 = new Version(package.VersionUpdate != null ? package.VersionUpdate.Name : package.Version.Name);
            var version2 = new Version(request.VersionNumber);
            if (version1.CompareTo(version2) >= 0)
            {
                throw new InvalidOperationException($"Package version should be higher than the latest one (v.{version1.ToString()})");
            }

            await versionRepository.CreateVersionAsync(package.Id, request.VersionNumber);
            return mapper.Map<Models.Package>(await packageRepository.GetPackageAsync(request.PackageId));
        }
    }
}
