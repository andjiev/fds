namespace FDS.Package.Service.Commands
{
    using MediatR;

    public class CreatePackageVersionCommand : IRequest<Models.Package>
    {
        public CreatePackageVersionCommand(int packageId, string versionNumber)
        {
            PackageId = packageId;
            VersionNumber = versionNumber;
        }

        public int PackageId { get; private set; }
        
        public string VersionNumber { get; private set; }
    }
}
