using DotnetAngularMiniEcommerce_API.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAngularMiniEcommerce_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts() {
            var products = _productService.GetProducts();
            return Ok(products);
        }
    }
}
