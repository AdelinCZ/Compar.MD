using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.QuestionQueries
{
    public class GetQuestionsQuery : IRequest<List<Question>>
    {
    }

    public class GetQuestionsHandler : IRequestHandler<GetQuestionsQuery, List<Question>>
    {
        private readonly AppDbContext context;

        public GetQuestionsHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<List<Question>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = await context.Questions.ToListAsync(cancellationToken);

            if (questions is null)
                return new();
            else
                return questions;
        }
    }
}