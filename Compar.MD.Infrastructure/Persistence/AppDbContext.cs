using Compar.MD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Compar.MD.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ProjectDescription> ProjectDescriptions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}