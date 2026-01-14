using EmployeeManagment.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagment.Services
{
    public class JwtService(IOptions<JwtOptions> _jwtOptions) 
    {

        public string GenerateAccessToken(ApplicationUser user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SiginingKey));
            var claims = new List<Claim>(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
        ]);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audince,
                claims: claims,
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.Value.LifeTime)
            );

            var accessToken = tokenHandler.WriteToken(token);

            return accessToken;
        }

      
    }

}
