namespace FDS.Package.Service.Commands
{
    using MediatR;
    using System.Collections.Generic;

    public class UpdatePackagesToInitialStateCommand : IRequest<List<Models.Package>>
    {
    }
}
