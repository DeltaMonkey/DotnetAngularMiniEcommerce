using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.ProductImageFiles.RemoveProductImage
{
    public class RemoveProductImageCommandRequest : IRequest<RemoveProductImageCommandResponse>
    {
        public string id { get; set; }
        public string? ImageId { get; set; }
    }
}
