using DotnetAngularMiniEcommerce_API.Domain.Entities;

namespace DotnetAngularMiniEcommerce_API.Application.Abstractions
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
