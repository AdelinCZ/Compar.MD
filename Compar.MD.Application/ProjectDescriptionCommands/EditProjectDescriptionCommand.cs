using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProjectDescriptionCommands
{
    public class EditProjectDescriptionCommand : IRequest<RequestStatus>
    {
        public string NewDescription { get; set; }
    }

    public class EditProjectDescriptionHandler : IRequestHandler<EditProjectDescriptionCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public EditProjectDescriptionHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(EditProjectDescriptionCommand request, CancellationToken cancellationToken)
        {
            var projectDescription = await context.ProjectDescriptions.FirstOrDefaultAsync(cancellationToken);

            if (projectDescription is null)
            {
                projectDescription = new()
                {
                    Description = request.NewDescription
                };

                await context.ProjectDescriptions.AddAsync(projectDescription, cancellationToken);
            }
            else
                projectDescription.Description = request.NewDescription;

            await context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}