namespace FDS.Package.Service.Queries
{
    using MediatR;
    using System.Collections.Generic;

    public class GetPackagesQuery : IRequest<List<Models.Package>>
    {
    }
}
