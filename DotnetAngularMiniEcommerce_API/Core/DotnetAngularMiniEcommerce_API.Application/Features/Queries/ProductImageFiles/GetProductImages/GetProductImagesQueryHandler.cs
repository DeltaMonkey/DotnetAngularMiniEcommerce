using DotnetAngularMiniEcommerce_API.Application.Repositories;
using DotnetAngularMiniEcommerce_API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Queries.ProductImageFiles.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        private readonly IConfiguration _configuration;
        private readonly IProductReadRepository _productReadRepository;

        public GetProductImagesQueryHandler(IConfiguration configuration, IProductReadRepository productReadRepository)
        {
            _configuration = configuration;
            _productReadRepository = productReadRepository;
        }

        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            //eager loading
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.ID == Guid.Parse(request.id));

            var products = product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                ID = p.ID
            }).ToList();

            return products ?? new List<GetProductImagesQueryResponse>();
        }
    }
}
