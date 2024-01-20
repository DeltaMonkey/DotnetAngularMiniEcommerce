using DotnetAngularMiniEcommerce_API.Application.Requestparameters;
using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public Pagination Pagination { get; set; }
    }
}
