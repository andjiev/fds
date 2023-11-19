namespace FDS.Package.Service.Commands
{
    using MediatR;
    using Models = FDS.Common.Models;

    public class UpdatePackageCommand : IRequest<Models.Package>
    {
        public UpdatePackageCommand(int packageId)
        {
            PackageId = packageId;
        }

        public int PackageId { get; private set; }
    }
}
