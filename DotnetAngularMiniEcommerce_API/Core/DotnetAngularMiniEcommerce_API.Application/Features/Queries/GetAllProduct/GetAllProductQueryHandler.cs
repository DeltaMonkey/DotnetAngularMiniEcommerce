using DotnetAngularMiniEcommerce_API.Application.Repositories;
using DotnetAngularMiniEcommerce_API.Application.Requestparameters;
using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false)
                .Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size)
                .Select(p => new
                {
                    p.ID,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                });

            return new GetAllProductQueryResponse()
            {
                TotalCount = totalCount,
                Products = products
            };
        }
    }
}
