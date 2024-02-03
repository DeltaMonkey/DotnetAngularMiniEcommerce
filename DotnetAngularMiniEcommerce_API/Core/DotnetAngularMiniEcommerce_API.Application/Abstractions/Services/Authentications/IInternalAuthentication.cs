using DotnetAngularMiniEcommerce_API.Application.DTOs;

namespace DotnetAngularMiniEcommerce_API.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<TokenDto> LoginAsync(string userNameOrEmail, string password, int accessTokenLifetime);
    }
}
