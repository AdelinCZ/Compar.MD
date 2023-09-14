using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProductTypeCommands
{
    public class DeleteProductTypeCommand : IRequest<RequestStatus>
    {
        public string ProductTypeId { get; set; }
    }

    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public DeleteProductTypeHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productTypeStatus = Guid.TryParse(request.ProductTypeId, out var productTypeId);

            if (productTypeStatus is false)
                return RequestStatus.Create("The product type id is wrong.");

            var existingProductType = await this.context.ProductTypes.FirstOrDefaultAsync(x => x.Id == productTypeId, cancellationToken);

            if (existingProductType is null)
                return RequestStatus.Create($"The product type with id {request.ProductTypeId} doesn't exist.");

            this.context.Remove(existingProductType);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}