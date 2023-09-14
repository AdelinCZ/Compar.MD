using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.SupllierCommands
{
    public class DeleteSupplierCommand : IRequest<RequestStatus>
    {
        public string SupplierId { get; set; }
    }

    public class DeletSupplierHandler : IRequestHandler<DeleteSupplierCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public DeletSupplierHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierStatus = Guid.TryParse(request.SupplierId, out var supplierId);

            if (supplierStatus is false)
                return RequestStatus.Create("The supplier id is wrong.");

            var existingSupplier = await this.context.Suppliers.FirstOrDefaultAsync(x => x.Id == supplierId, cancellationToken);

            if (existingSupplier is null)
                return RequestStatus.Create($"The supplier with id {request.SupplierId} doesn't exist.");

            this.context.Suppliers.Remove(existingSupplier);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}