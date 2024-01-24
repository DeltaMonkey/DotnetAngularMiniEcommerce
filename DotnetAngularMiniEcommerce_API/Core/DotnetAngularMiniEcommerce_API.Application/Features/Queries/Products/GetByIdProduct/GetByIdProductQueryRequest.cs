using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Queries.Products.GetByIdProduct
{
    public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
    {
        public string id { get; set; }
    }
}
