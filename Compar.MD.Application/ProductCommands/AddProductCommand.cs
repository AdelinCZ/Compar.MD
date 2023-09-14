using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProductCommands
{
    public class AddProductCommand : IRequest<RequestStatus>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string ImageURL { get; set; }
        public List<(string Name, decimal Price)> Suppliers { get; set; } = new();
    }

    public class AddProductHandler : IRequestHandler<AddProductCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public AddProductHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await this.context.Products.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (existing != null)
                return RequestStatus.Create("A product with this name already exist.");

            var brandStatus = await this.context.Brands.AnyAsync(x => x.Name == request.Brand, cancellationToken);

            if (!brandStatus)
                return RequestStatus.Create("The brand doesn't exist.");

            var typeStatus = await this.context.ProductTypes.AnyAsync(x => x.Name == request.Type, cancellationToken);

            if (!typeStatus)
                return RequestStatus.Create("The product type doesn't exist.");

            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageURL = request.ImageURL,
                Brand = request.Brand,
                Type = request.Type,
                LastUpdated = DateTime.UtcNow,
            };

            foreach (var (Name, Price) in request.Suppliers)
            {
                var supplierStatus = await this.context.Stores.AnyAsync(x => x.Name == Name, cancellationToken);

                if (!supplierStatus && !string.IsNullOrEmpty(Name))
                    return RequestStatus.Create("The product type doesn't exist.");

                var store = new Supplier()
                {
                    StoreName = Name,
                    Price = Price,
                    LastUpdated = DateTime.UtcNow,
                    ProductId = product.Id
                };

                product.Suppliers.Add(store);
            }

            await this.context.AddAsync(product, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}