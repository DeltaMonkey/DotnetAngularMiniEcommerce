using Microsoft.AspNetCore.Identity;

namespace DotnetAngularMiniEcommerce_API.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
