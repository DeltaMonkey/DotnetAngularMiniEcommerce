using DotnetAngularMiniEcommerce_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetAngularMiniEcommerce_API.Persistence.Contexts
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
