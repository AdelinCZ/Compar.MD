using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.BrandCommands
{
    public class AddBrandCommand : IRequest<RequestStatus>
    {
        public string BrandName { get; set; }
    }

    public class AddBrandHandler : IRequestHandler<AddBrandCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public AddBrandHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await this.context.Brands.FirstOrDefaultAsync(x => x.Name == request.BrandName, cancellationToken);

            if (existingBrand is not null)
                return RequestStatus.Create("A brand with this name already exist.");

            var brand = new Brand() { Name = request.BrandName };

            await this.context.Brands.AddAsync(brand, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}