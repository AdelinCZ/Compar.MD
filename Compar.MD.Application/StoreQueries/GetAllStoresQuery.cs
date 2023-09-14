using Compar.MD.Domain.Models;
using Compar.MD.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Application.StoreQueries
{
    public class GetAllStoresQuery : IRequest<List<Store>>
    {
    }

    public class GetAllStoresHandler : IRequestHandler<GetAllStoresQuery, List<Store>>
    {
        private readonly AppDbContext context;

        public GetAllStoresHandler(AppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<List<Store>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
        {
            var stores = await this.context.Stores.ToListAsync(cancellationToken);

            return stores;
        }
    }
}