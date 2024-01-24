using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.Products.RemoveProduct
{
    public class RemoveProductCommandRequest : IRequest<RemoveProductCommandResponse>
    {
        public string id { get; set; }
    }
}
