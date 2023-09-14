using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.StoreCommands
{
    public class AddStoreCommand : IRequest<RequestStatus>
    {
        public string StoreName { get; set; }
    }

    public class AddStoreHandler : IRequestHandler<AddStoreCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public AddStoreHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(AddStoreCommand request, CancellationToken cancellationToken)
        {
            var existingStore = await this.context.Stores.FirstOrDefaultAsync(x => x.Name == request.StoreName, cancellationToken);

            if (existingStore is not null)
                return RequestStatus.Create("A store with this name already exist.");

            var store = new Store() { Name = request.StoreName };

            await this.context.Stores.AddAsync(store, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}