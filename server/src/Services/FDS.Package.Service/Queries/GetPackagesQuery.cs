namespace FDS.Package.Service.Queries
{
    using MediatR;
    using System.Collections.Generic;
    using Models = FDS.Common.Models;

    public class GetPackagesQuery : IRequest<List<Models.Package>>
    {
    }
}
