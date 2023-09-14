using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.QuestionCommands
{
    public class EditQuestionCommand : IRequest<RequestStatus>
    {
        public string Id { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class EditQuestionHandler : IRequestHandler<EditQuestionCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public EditQuestionHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == string.Empty)
                return RequestStatus.Create("Id ul intrebarii este invalid");

            var result = Guid.TryParse(request.Id, out var id);

            if (result is false)
                return RequestStatus.Create("Id ul intrebarii este invalid");

            if (request.Question == string.Empty)
                return RequestStatus.Create("Continutul intrebarii este invalid");

            if (request.Answer == string.Empty)
                return RequestStatus.Create("Continutul raspunsului este invalid");

            var question = await context.Questions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (question is null)
                return RequestStatus.Create("Intrebare cu asemenea ID nu exista");

            question.Value = request.Question;
            question.Answer = request.Answer;

            await context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}