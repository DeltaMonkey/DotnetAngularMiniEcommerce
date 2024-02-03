using DotnetAngularMiniEcommerce_API.Application.DTOs;

namespace DotnetAngularMiniEcommerce_API.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(int second);
    }
}
