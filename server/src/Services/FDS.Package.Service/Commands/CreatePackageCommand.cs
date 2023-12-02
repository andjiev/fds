namespace FDS.Package.Service.Commands
{
    using FDS.Common.DataContext.Enums;
    using MediatR;
    using Models = FDS.Common.Models;

    public class CreatePackageCommand : IRequest<Unit>
    {
        public CreatePackageCommand(string packageName, string packageDescription, string packageVersion, PackageType packageType)
        {
            PackageName = packageName;
            PackageDescription = packageDescription;
            PackageVersion = packageVersion;
            PackageType = packageType;
        }

        public string PackageName { get; }

        public string PackageDescription { get; }

        public string PackageVersion { get; }

        public PackageType PackageType { get; }
    }
}
