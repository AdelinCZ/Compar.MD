using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;

namespace Compar.MD.Application.QuestionCommands
{
    public class AddQuestionCommand : IRequest<RequestStatus>
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class AddQuestionHandler : IRequestHandler<AddQuestionCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public AddQuestionHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request.Question == string.Empty)
                return RequestStatus.Create("Continutul intrebarii este invalid");

            if (request.Answer == string.Empty)
                return RequestStatus.Create("Continutul raspunsului este invalid");

            var question = new Question()
            {
                Value = request.Question,
                Answer = request.Answer
            };

            await context.Questions.AddAsync(question, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}