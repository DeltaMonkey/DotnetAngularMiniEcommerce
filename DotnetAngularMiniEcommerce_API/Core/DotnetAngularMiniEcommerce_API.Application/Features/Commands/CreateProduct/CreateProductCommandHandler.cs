using DotnetAngularMiniEcommerce_API.Application.Repositories;
using DotnetAngularMiniEcommerce_API.Domain.Entities;
using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
