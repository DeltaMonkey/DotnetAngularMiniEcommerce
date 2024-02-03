using DotnetAngularMiniEcommerce_API.Application.Abstractions.Services;
using DotnetAngularMiniEcommerce_API.Application.Abstractions.Token;
using DotnetAngularMiniEcommerce_API.Application.DTOs;
using DotnetAngularMiniEcommerce_API.Application.Exceptions;
using DotnetAngularMiniEcommerce_API.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DotnetAngularMiniEcommerce_API.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<TokenDto> LoginAsync(string userNameOrEmail, string password, int accessTokenLifetime)
        {
            AppUser appUser = await _userManager.FindByNameAsync(userNameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(userNameOrEmail);

            if (appUser == null)
                throw new NotFoundUserException("UserName or password is wrong...");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);
            if (result.Succeeded) //Authentication is succeeded
            {
                TokenDto tokenDto = _tokenHandler.CreateAccessToken(accessTokenLifetime);
                return tokenDto;
            }

            throw new AuthenticationErrorException();
        }
    }
}
