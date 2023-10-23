namespace FDS.Package.Service.Commands
{
    using MediatR;
    using System.Collections.Generic;

    public class UpdateAllPackagesCommand : IRequest<List<Models.Package>>
    {
    }
}
