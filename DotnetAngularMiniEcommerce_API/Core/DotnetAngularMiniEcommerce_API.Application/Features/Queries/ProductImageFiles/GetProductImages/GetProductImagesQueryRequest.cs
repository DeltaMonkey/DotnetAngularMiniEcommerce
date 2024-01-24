using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Queries.ProductImageFiles.GetProductImages
{
    public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
    {
        public string id { get; set; }
    }
}
