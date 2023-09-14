using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.BrandQueries
{
    public class GetAllBrandsQuery : IRequest<List<Brand>>
    {
    }

    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, List<Brand>>
    {
        private readonly AppDbContext context;

        public GetAllBrandsHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<List<Brand>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await this.context.Brands.ToListAsync(cancellationToken);

            return brands;
        }
    }
}