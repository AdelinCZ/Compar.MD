using Compar.MD.Application.ProductQueries;
using Compar.MD.Infrastructure;

namespace Compar.MD.Server
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddComposition(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
                config.RegisterServicesFromAssembly(typeof(GetAllProductsQuery).Assembly);
            });

            return services;
        }
    }
}