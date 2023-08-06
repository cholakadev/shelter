using Microsoft.IdentityModel.Tokens;
using SharedKernel.Models.Settings;
using SharedKernel.Services.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Infrastructure.Domain;

namespace Users.Core.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateJwtToken(string userEmail, Guid userId, TokenSettings settings, int expiresInMinutes = 60)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.Secret);

            var claims = new ClaimsIdentity(new[]
            {
            new Claim(AuthConstants.UserIdType, userId.ToString()),
            new Claim(AuthConstants.EmailType, userEmail),
        });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = settings.Issuer,
                Audience = settings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
