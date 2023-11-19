namespace FDS.Api.Controllers
{
    using FDS.Package.Service.Commands;
    using FDS.Package.Service.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;

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

        [HttpPut("sync")]
        public async Task<ActionResult> InitializePackages()
        {
            await mediator.Send(new SyncPackagesCommand());
            return Ok();
        }

        [HttpPut("{packageId:int}")]
        public async Task<ActionResult<Models.Package>> UpdatePackageVersion(int packageId)
        {
            var package = await mediator.Send(new UpdatePackageCommand(packageId));
            return Ok(package);
        }

        [HttpPut("updateAll")]
        public async Task<ActionResult<List<Models.Package>>> UpdateAllPackages()
        {
            var packages = await mediator.Send(new UpdateAllPackagesCommand());
            return Ok(packages);
        }
    }
}
