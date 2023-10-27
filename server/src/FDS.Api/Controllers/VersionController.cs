namespace FDS.Api.Controllers
{
    using FDS.Package.Service.Commands;
    using FDS.Package.Service.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models = Package.Service.Models;

    [Route("api/versions")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IMediator mediator;

        public VersionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Models.Package>> UpdatePackageVersion([FromBody]Models.CreateVersion model)
        {
            var package = await mediator.Send(new CreatePackageVersionCommand(model.PackageId, model.VersionNumber));
            return Ok(package);
        }
    }
}
