using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.AppUsers.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
