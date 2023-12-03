namespace FDS.Package.Service.Commands
{
    using MediatR;
    using System.Collections.Generic;
    using Models = FDS.Common.Models;

    public class UpdateSelectedPackagesCommand : IRequest<List<Models.Package>>
    {
        public UpdateSelectedPackagesCommand(List<int> packageIds)
        {
            PackageIds = packageIds;
        }

        public List<int> PackageIds { get; }
    }
}
