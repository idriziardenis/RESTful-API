using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Shared
{
    public class TokenProvider
    {
        public static string Secret { get; set; }

        public static string GenerateToken(string username, string userId, int defaultExpireTime = 20)
        {
            var secretBytes = Encoding.ASCII.GetBytes(Secret);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SymmetricSecurityKey key = new SymmetricSecurityKey(secretBytes);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, username),
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.Now.AddMinutes(defaultExpireTime),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            return handler.WriteToken(token);
        }
    }
}
