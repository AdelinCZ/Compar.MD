using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProductCommands
{
    public class DeleteProductCommand : IRequest<RequestStatus>
    {
        public string ProductId { get; set; }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public DeleteProductHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productStatus = Guid.TryParse(request.ProductId, out var productId);

            if (productStatus is false)
                return RequestStatus.Create("The product id is wrong.");

            var existingProduct = await this.context.Products.FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

            if (existingProduct is null)
                return RequestStatus.Create($"The product with id {request.ProductId} doesn't exist.");

            this.context.Remove(existingProduct);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}