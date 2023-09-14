using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.SupllierCommands
{
    public class AddSupplierCommand : IRequest<RequestStatus>
    {
        public string ProductId { get; set; }
        public string StoreName { get; set; }
        public decimal Price { get; set; }
    }

    public class AddSupplierHandler : IRequestHandler<AddSupplierCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public AddSupplierHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
        {
            var existingStore = await this.context.Stores.FirstOrDefaultAsync(x => x.Name == request.StoreName);

            if (existingStore is null)
                return RequestStatus.Create("A store with this name doesn't exist. Try to add it first.");

            var productStatus = Guid.TryParse(request.ProductId, out var productId);

            if (productStatus is false)
                return RequestStatus.Create("The product id is wrong.");

            var existingSupplier = await this.context.Suppliers
                .FirstOrDefaultAsync(x => x.StoreName == request.StoreName && x.ProductId == productId, cancellationToken);

            if (existingSupplier is not null)
                return RequestStatus.Create("This supplier already exist for this product. Try to edit it.");

            var product = await this.context.Products.Where(x => x.Id == productId).Include(x => x.Suppliers)
                .FirstOrDefaultAsync(cancellationToken);

            var supplier = new Supplier()
            {
                StoreName = request.StoreName,
                Price = request.Price,
                ProductId = product.Id
            };

            product.Suppliers.Add(supplier);

            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}