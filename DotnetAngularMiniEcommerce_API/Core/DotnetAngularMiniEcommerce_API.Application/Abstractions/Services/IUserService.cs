using DotnetAngularMiniEcommerce_API.Application.DTOs.Users;
using DotnetAngularMiniEcommerce_API.Domain.Entities.Identity;

namespace DotnetAngularMiniEcommerce_API.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
        Task UpdateRefreshToken(string refreshToken, DateTime accessTokenDate, int refreshTokenLifeTimeSecond, AppUser user);
    }
}
