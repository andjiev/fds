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

        [HttpPost]
        public async Task<ActionResult> AddPackage(Models.PackageToAdd package)
        {
            await mediator.Send(new CreatePackageCommand(package.Name, package.Description, package.Version, package.Type));
            return Ok();
        }

        [HttpPut("{packageId:int}")]
        public async Task<ActionResult<Models.Package>> UpdatePackageVersion(int packageId)
        {
            var package = await mediator.Send(new UpdatePackageCommand(packageId));
            return Ok(package);
        }

        [HttpPut("import")]
        public async Task<ActionResult> ImportPackages()
        {
            await mediator.Send(new ImportPackagesCommand());
            return Ok();
        }

        [HttpPut("updateSelected")]
        public async Task<ActionResult<List<Models.Package>>> UpdateSelectedPackages(List<int> packageIds)
        {
            var packages = await mediator.Send(new UpdateSelectedPackagesCommand(packageIds));
            return Ok(packages);
        }

        [HttpPut("deleteSelected")]
        public async Task<ActionResult<List<Models.Package>>> DeleteSelectedPackages(List<int> packageIds)
        {
            var packages = await mediator.Send(new DeleteSelectedPackagesCommand(packageIds));
            return Ok(packages);
        }
    }
}
