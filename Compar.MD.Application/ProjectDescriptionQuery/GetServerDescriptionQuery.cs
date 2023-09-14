using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProjectDescriptionQuery
{
    public class GetServerDescriptionQuery : IRequest<ProjectDescription>
    {
    }

    public class GetServerDescriptionHandler : IRequestHandler<GetServerDescriptionQuery, ProjectDescription>
    {
        private readonly AppDbContext context;

        public GetServerDescriptionHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<ProjectDescription> Handle(GetServerDescriptionQuery request, CancellationToken cancellationToken)
        {
            var projectDescription = await context.ProjectDescriptions.FirstOrDefaultAsync(cancellationToken);

            if (projectDescription is null)
                return new();
            else
                return projectDescription;
        }
    }
}