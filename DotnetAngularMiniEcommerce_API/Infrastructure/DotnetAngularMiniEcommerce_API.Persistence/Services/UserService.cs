using DotnetAngularMiniEcommerce_API.Application.Abstractions.Services;
using DotnetAngularMiniEcommerce_API.Application.DTOs.Users;
using DotnetAngularMiniEcommerce_API.Application.Exceptions;
using DotnetAngularMiniEcommerce_API.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DotnetAngularMiniEcommerce_API.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
        {
            IdentityResult result = await _userManager.CreateAsync(new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.NameSurname,
            }, model.Password);

            CreateUserResponseDto response = new CreateUserResponseDto() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarı ile oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message = $"{error.Code} - {error.Description}<br>";

            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, DateTime accessTokenDate, int refreshTokenLifeTimeSecond, AppUser user)
        {
            if (user != null)
            { 
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenLifeTimeSecond);
                await _userManager.UpdateAsync(user);
                return;
            }
            throw new NotFoundUserException();
        }
    }
}
