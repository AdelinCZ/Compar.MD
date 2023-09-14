using Compar.MD.Application.ProjectDescriptionCommands;
using Compar.MD.Application.ProjectDescriptionQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Compar.MD.Server.Controllers
{
    [Route("api/server-description")]
    [ApiController]
    public class ProjectDescriptionController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProjectDescriptionController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);

            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<string> GetProjectDescription()
        {
            var projectDescription = await mediator.Send(new GetServerDescriptionQuery());

            return projectDescription.Description;
        }

        [HttpPost]
        public async Task<IActionResult> EditProjectDescription([FromBody] string data)
        {
            var command = new EditProjectDescriptionCommand()
            {
                NewDescription = data
            };

            var result = await mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }
    }
}