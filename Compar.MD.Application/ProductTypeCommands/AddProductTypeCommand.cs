using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProductTypeCommands
{
    public class AddProductTypeCommand : IRequest<RequestStatus>
    {
        public string ProductTypeName { get; set; }
    }

    public class AddProductTypeHandler : IRequestHandler<AddProductTypeCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public AddProductTypeHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(AddProductTypeCommand request, CancellationToken cancellationToken)
        {
            var existingProductType = await this.context.ProductTypes.FirstOrDefaultAsync(x => x.Name == request.ProductTypeName, cancellationToken);

            if (existingProductType is not null)
                return RequestStatus.Create("A product type with this name already exist.");

            var productType = new ProductType() { Name = request.ProductTypeName };

            await this.context.ProductTypes.AddAsync(productType, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}