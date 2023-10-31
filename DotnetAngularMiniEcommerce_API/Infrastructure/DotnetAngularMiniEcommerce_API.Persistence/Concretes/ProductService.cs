using DotnetAngularMiniEcommerce_API.Application.Abstractions;
using DotnetAngularMiniEcommerce_API.Domain.Entities;

namespace DotnetAngularMiniEcommerce_API.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts() 
            => new()
            { 
                new() { ID = Guid.NewGuid(), Name = "Product 1", CreatedDate = DateTime.Now, Price = 100, Stock = 10 },
                new() { ID = Guid.NewGuid(), Name = "Product 2", CreatedDate = DateTime.Now, Price = 200, Stock = 10 },
                new() { ID = Guid.NewGuid(), Name = "Product 3", CreatedDate = DateTime.Now, Price = 300, Stock = 10 },
                new() { ID = Guid.NewGuid(), Name = "Product 4", CreatedDate = DateTime.Now, Price = 400, Stock = 10 },
                new() { ID = Guid.NewGuid(), Name = "Product 5", CreatedDate = DateTime.Now, Price = 500, Stock = 10 }
            };
    }
}
