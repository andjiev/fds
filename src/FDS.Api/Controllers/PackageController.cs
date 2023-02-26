namespace FDS.Api.Controllers
{
    using FDS.Package.Service.Commands;
    using FDS.Package.Service.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models = Package.Service.Models;

    [Route("api/packages")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IMediator mediator;

        public PackageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Package>>> GetPackages()
        {
            var packages = await mediator.Send(new GetPackagesQuery());
            return Ok(packages);
        }

        [HttpPatch("{packageId:int}/version/{versionId:int}")]
        public async Task<ActionResult<Models.Package>> UpdatePackageVersion(int packageId, int versionId)
        {
            var package = await mediator.Send(new UpdatePackageVersionCommand(packageId, versionId));
            return Ok(package);
        }

        [HttpPatch("reset")]
        public async Task<ActionResult<List<Models.Package>>> UpdatePackagesToInitialState()
        {
            var packages = await mediator.Send(new UpdatePackagesToInitialStateCommand());
            return Ok(packages);
        }
    }
}
