using Compar.MD.Application.QuestionCommands;
using Compar.MD.Application.QuestionQueries;
using Compar.MD.Server.Mapping;
using Compar.MD.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Compar.MD.Server.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuestionsController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);

            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<QuestionDto>> GetQuestions()
        {
            var questions = await mediator.Send(new GetQuestionsQuery());
            var dto = Mapper.GetQuestions(questions);

            return dto;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionDto data)
        {
            var command = new AddQuestionCommand()
            {
                Question = data.Question,
                Answer = data.Answer
            };

            var result = await mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditQuestion(QuestionDto data)
        {
            var command = new EditQuestionCommand()
            {
                Id = data.Id,
                Question = data.Question,
                Answer = data.Answer
            };

            var result = await mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteQuestion([FromBody] string id)
        {
            var command = new DeleteQuestionCommand()
            {
                Id = id,
            };

            var result = await mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();
            else
                return BadRequest(result.Error);
        }
    }
}