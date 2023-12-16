using DotnetAngularMiniEcommerce_API.Application.Repositories;
using DotnetAngularMiniEcommerce_API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAngularMiniEcommerce_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IOrderWriteRepository orderWriteRepository,
            ICustomerWriteRepository customerWriteRepository,
            IOrderReadRepository orderReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        [HttpGet]
        public async Task Get() 
        {
            var order = await _orderReadRepository.GetByIdAsync("592296e9-f7ed-4546-a9d2-9aba5bfc31f2", false);
            order.Address = "Gonya";
            _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveAsync();
        }
    }
}
