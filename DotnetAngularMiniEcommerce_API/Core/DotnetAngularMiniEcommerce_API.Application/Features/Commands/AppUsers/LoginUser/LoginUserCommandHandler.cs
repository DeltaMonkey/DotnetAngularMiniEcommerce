using DotnetAngularMiniEcommerce_API.Application.Abstractions.Token;
using DotnetAngularMiniEcommerce_API.Application.DTOs;
using DotnetAngularMiniEcommerce_API.Application.Exceptions;
using DotnetAngularMiniEcommerce_API.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.AppUsers.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser appUser = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if(appUser == null)
                appUser = await _userManager.FindByEmailAsync(request.UserNameOrEmail);

            if (appUser == null)
                throw new NotFoundUserException("UserName or password is wrong...");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);
            if (result.Succeeded) //Authentication is succeeded
            {
                TokenDto tokenDto = _tokenHandler.CreateAccessToken(5);
                return new()
                {
                    TokenDto = tokenDto
                };
            }

            throw new AuthenticationErrorException();
        }
    }
}
