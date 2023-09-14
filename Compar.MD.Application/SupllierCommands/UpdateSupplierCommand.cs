using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.SupllierCommands
{
    public class UpdateSupplierCommand : IRequest<RequestStatus>
    {
        public string SupplierId { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public UpdateSupplierHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierStatus = Guid.TryParse(request.SupplierId, out var supplierId);

            if (supplierStatus is false)
                return RequestStatus.Create("The supplier id is wrong.");

            var existingSupplier = await this.context.Suppliers.FirstOrDefaultAsync(x => x.Id == supplierId, cancellationToken);

            if (existingSupplier is null)
                return RequestStatus.Create($"The supplier with id {supplierId} doesn't exist.");

            existingSupplier.Update(request.Price);

            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}