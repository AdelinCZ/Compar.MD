using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.QuestionCommands
{
    public class DeleteQuestionCommand : IRequest<RequestStatus>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class DeleteQuestionHandler : IRequestHandler<DeleteQuestionCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public DeleteQuestionHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == string.Empty)
                return RequestStatus.Create("Id ul intrebarii este invalid");

            var result = Guid.TryParse(request.Id, out var id);

            if (result is false)
                return RequestStatus.Create("Id ul intrebarii este invalid");

            var question = await context.Questions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (question is null)
                return RequestStatus.Create("Intrebare cu asemenea ID nu exista");

            context.Questions.Remove(question);
            await context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}