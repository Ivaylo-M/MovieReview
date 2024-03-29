﻿namespace Application.Services
{
    using Application.Services.Contracts;
    using Domain;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    public class TokenService : ITokenService
    {
        public string CreateToken(User user)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            string secretKey = GenerateKey();
            byte[] bytes = Encoding.UTF8.GetBytes(secretKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(bytes);

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }

        public string GenerateKey()
        {
            byte[] rdmNumber = new byte[32];
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(rdmNumber);
                return Convert.ToBase64String(rdmNumber);
            }
        }
    }
}
