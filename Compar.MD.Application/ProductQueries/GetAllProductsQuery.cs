using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProductQueries
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly AppDbContext context;

        public GetAllProductsHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await this.context.Products.Include(x => x.Suppliers).ToListAsync(cancellationToken);

            return products;
        }
    }
}