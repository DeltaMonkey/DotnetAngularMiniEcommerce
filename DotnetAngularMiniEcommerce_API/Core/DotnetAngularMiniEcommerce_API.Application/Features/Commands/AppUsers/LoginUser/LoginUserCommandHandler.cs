using DotnetAngularMiniEcommerce_API.Application.Abstractions.Services;
using DotnetAngularMiniEcommerce_API.Application.DTOs;
using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.AppUsers.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            TokenDto token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password, 15);
            return new LoginUserCommandResponse { TokenDto = token };
        }
    }
}
