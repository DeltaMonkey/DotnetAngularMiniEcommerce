﻿using DotnetAngularMiniEcommerce_API.Application.Abstractions.Token;
using DotnetAngularMiniEcommerce_API.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace DotnetAngularMiniEcommerce_API.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto CreateAccessToken(int second)
        {
            TokenDto tokenDto = new TokenDto();

            // Security keyin simetrigini aliyoruz
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            // Sifrelenmis kimligi olusturuyoruz
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Olusturulmus token ayarlarini veriyoruz
            tokenDto.Expiration = DateTime.UtcNow.AddSeconds(second);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: tokenDto.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );

            // Token olusturucu sinifindan bir ornek alalim
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenDto.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

            tokenDto.RefreshToken = CreateRefreshToken();

            return tokenDto;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
