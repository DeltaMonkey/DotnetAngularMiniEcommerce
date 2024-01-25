using MediatR;

namespace DotnetAngularMiniEcommerce_API.Application.Features.Commands.AppUsers
{
    public class CreateAppUserCommandRequest : IRequest<CreateAppUserCommandResponse>
    {
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string passwordRepeat { get; set; }
    }
}
