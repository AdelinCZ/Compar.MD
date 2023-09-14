using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProductQueries
{
    public class GetSelectedProductsQuery : IRequest<List<Product>>
    {
    }

    public class GetSelectedProductsHandler : IRequestHandler<GetSelectedProductsQuery, List<Product>>
    {
        private readonly AppDbContext context;

        public GetSelectedProductsHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<List<Product>> Handle(GetSelectedProductsQuery request, CancellationToken cancellationToken)
        {
            var selectedProducts = await this.context.Products.Include(x => x.Suppliers.OrderBy(x => x.Price))
                .Where(x => x.IsSelected == true).ToListAsync(cancellationToken);

            return selectedProducts;
        }
    }
}