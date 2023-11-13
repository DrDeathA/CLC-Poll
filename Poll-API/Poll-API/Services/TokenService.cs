using Poll_API.DTOs;
using Poll_API.Interfaces;
using System.Text.Json;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Poll_API.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Poll_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey secretKey;

        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name + " " + user.Surname),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(90),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
