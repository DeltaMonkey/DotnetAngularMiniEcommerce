﻿namespace DotnetAngularMiniEcommerce_API.Application.DTOs.Users
{
    public class CreateUserDto
    {
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
    }
}
