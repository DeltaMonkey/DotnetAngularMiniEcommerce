using DotnetAngularMiniEcommerce_API.Application.Requestparameters;
using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Queries.Products.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public Pagination Pagination { get; set; }
    }
}
