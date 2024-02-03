using DotnetAngularMiniEcommerce_API.Application.Abstractions.Services;
using DotnetAngularMiniEcommerce_API.Application.DTOs.Users;
using DotnetAngularMiniEcommerce_API.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.AppUsers.CreateUser
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, CreateAppUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateAppUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponseDto response = await _userService.CreateAsync(new CreateUserDto { 
                Email = request.Email,
                NameSurname = request.NameSurname, 
                Password = request.Password, 
                PasswordRepeat = request.PasswordRepeat, 
                UserName = request.UserName
            });

            return new CreateAppUserCommandResponse { 
                Message = response.Message,
                Succeeded = response.Succeeded
            };
        }
    }
}
