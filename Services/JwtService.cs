using Microsoft.IdentityModel.Tokens;
using SIADAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SIADAL.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(user user, string role)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new List<Claim>
        {
            new Claim("ID", user.id.ToString()),
            new Claim(ClaimTypes.Role, role), // Rol
            new Claim(JwtRegisteredClaimNames.Sub, user.email), // sub
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // jti
        };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256 // HS256
            );

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"], // iss
                audience: jwtSettings["Audience"], // aud
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(jwtSettings["DurationInMinutes"])
                ), // exp
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
