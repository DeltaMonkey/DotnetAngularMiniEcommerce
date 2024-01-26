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

        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                //... we will add the authorization
            }

            return new();
        }
    }
}
