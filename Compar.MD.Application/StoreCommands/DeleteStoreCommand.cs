using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.StoreCommands
{
    public class DeleteStoreCommand : IRequest<RequestStatus>
    {
        public string StoreId { get; set; }
    }

    public class DeleteStoreHandler : IRequestHandler<DeleteStoreCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public DeleteStoreHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var storeStatus = Guid.TryParse(request.StoreId, out var storeId);

            if (storeStatus is false)
                return RequestStatus.Create("The store id is wrong.");

            var existingStore = await this.context.Stores.FirstOrDefaultAsync(x => x.Id == storeId);

            if (existingStore is null)
                return RequestStatus.Create($"The store with id {request.StoreId} doesn't exist.");

            this.context.Remove(existingStore);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}