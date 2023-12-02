namespace FDS.Package.Service.Commands
{
    using MediatR;
    using System.Collections.Generic;
    using Models = FDS.Common.Models;

    public class UpdateAllPackagesCommand : IRequest<List<Models.Package>>
    {
    }
}
