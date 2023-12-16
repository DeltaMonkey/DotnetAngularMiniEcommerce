using DotnetAngularMiniEcommerce_API.Application.Repositories;
using DotnetAngularMiniEcommerce_API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAngularMiniEcommerce_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(
            IProductWriteRepository productWriteRepository, 
            IProductReadRepository productReadRepository
            )
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task Get() 
        {
            ///await _productWriteRepository.AddRangeAsync(new()
            ///{
            ///    new() { ID = Guid.NewGuid(), Name = "Product_1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10 },
            ///    new() { ID = Guid.NewGuid(), Name = "Product_2", Price = 200, CreatedDate = DateTime.UtcNow, Stock = 20 },
            ///    new() { ID = Guid.NewGuid(), Name = "Product_3", Price = 300, CreatedDate = DateTime.UtcNow, Stock = 130 }
            ///});
            ///await _productWriteRepository.SaveAsync();
            Product p = await _productReadRepository.GetByIdAsync("879af56d-0fbf-41e4-a17c-86cf90983acd", false);
            p.Name = "ABC";
            await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
