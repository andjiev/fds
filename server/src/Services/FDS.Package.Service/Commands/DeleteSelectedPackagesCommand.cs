namespace FDS.Package.Service.Commands
{
    using MediatR;
    using System.Collections.Generic;
    using Models = FDS.Common.Models;

    public class DeleteSelectedPackagesCommand : IRequest<Unit>
    {
        public DeleteSelectedPackagesCommand(List<int> packageIds)
        {
            PackageIds = packageIds;
        }

        public List<int> PackageIds { get; }
    }
}
