using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.BrandCommands
{
    public class DeleteBrandCommand : IRequest<RequestStatus>
    {
        public string BrandId { get; set; }
    }

    public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public DeleteBrandHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brandStatus = Guid.TryParse(request.BrandId, out var brandId);

            if (brandStatus is false)
                return RequestStatus.Create("The brand id is wrong.");

            var existingBrand = await this.context.Brands.FirstOrDefaultAsync(x => x.Id == brandId, cancellationToken);

            if (existingBrand is null)
                return RequestStatus.Create($"The brand with id {request.BrandId} doesn't exist.");

            this.context.Remove(existingBrand);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}