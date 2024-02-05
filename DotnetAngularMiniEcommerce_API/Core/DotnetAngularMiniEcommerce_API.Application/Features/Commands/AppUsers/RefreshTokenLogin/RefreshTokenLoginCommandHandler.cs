using DotnetAngularMiniEcommerce_API.Application.Abstractions.Services;
using DotnetAngularMiniEcommerce_API.Application.DTOs;
using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.AppUsers.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            TokenDto tokenDto = await _authService.RefreshLoginAsync(request.RefreshToken, 15);
            return new RefreshTokenLoginCommandResponse { TokenDto = tokenDto };
        }
    }
}
