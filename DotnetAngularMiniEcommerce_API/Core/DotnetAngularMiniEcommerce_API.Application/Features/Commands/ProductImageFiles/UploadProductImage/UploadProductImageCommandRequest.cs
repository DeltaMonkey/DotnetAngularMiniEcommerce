using MediatR;
using Microsoft.AspNetCore.Http;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.ProductImageFiles.UploadProductImage
{
    public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse>
    {
        public string id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
