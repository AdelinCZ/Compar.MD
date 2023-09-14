using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.ProductCommands
{
    public class SelectProductCommand : IRequest<RequestStatus>
    {
        public List<string> NewSelectedProducts { get; set; }
    }

    public class SelectProductHandler : IRequestHandler<SelectProductCommand, RequestStatus>
    {
        private readonly AppDbContext context;

        public SelectProductHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<RequestStatus> Handle(SelectProductCommand request, CancellationToken cancellationToken)
        {
            var existingSelectedItems = await this.context.Products.Where(x => x.IsSelected == true).ToListAsync(cancellationToken);

            foreach (var existingSelectedItem in existingSelectedItems)
            {
                existingSelectedItem.IsSelected = false;
            }

            foreach (var newSelectedProduct in request.NewSelectedProducts)
            {
                var productIdStatus = Guid.TryParse(newSelectedProduct, out var productId);

                if (productIdStatus is true)
                {
                    var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

                    if (product is not null)
                        product.IsSelected = true;
                }
            }

            await this.context.SaveChangesAsync(cancellationToken);

            return new RequestStatus();
        }
    }
}