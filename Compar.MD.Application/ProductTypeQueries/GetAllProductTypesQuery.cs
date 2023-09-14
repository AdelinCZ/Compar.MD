using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProductTypeQueries
{
    public class GetAllProductTypesQuery : IRequest<List<ProductType>>
    {
    }

    public class GetAllProductTypesHandlers : IRequestHandler<GetAllProductTypesQuery, List<ProductType>>
    {
        private readonly AppDbContext context;

        public GetAllProductTypesHandlers(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<List<ProductType>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await this.context.ProductTypes.ToListAsync(cancellationToken);

            return productTypes;
        }
    }
}