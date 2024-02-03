using DotnetAngularMiniEcommerce_API.Application.DTOs.Users;

namespace DotnetAngularMiniEcommerce_API.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
    }
}
