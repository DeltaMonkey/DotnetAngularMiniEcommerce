using DotnetAngularMiniEcommerce_API.Application.Repositories;
using DotnetAngularMiniEcommerce_API.Domain.Entities;
using DotnetAngularMiniEcommerce_API.Persistence.Contexts;

namespace DotnetAngularMiniEcommerce_API.Persistence.Repositories
{
    public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
