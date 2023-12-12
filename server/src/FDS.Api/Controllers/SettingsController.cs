namespace FDS.Api.Controllers
{
    using FDS.Package.Service.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;

    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SettingsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Models.Settings>> GetSettings()
        {
            var settings = await mediator.Send(new GetSettingsQuery());
            return Ok(settings);
        }
    }
}
