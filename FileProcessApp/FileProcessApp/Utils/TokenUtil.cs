using FileProcessingApp.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using FileProcessApp.Model;
using FileProcessApp.Services.Interface;

namespace FileProcessingApp.Utils
{
    public class TokenUtil: ITokenUtil
    {
        public readonly IConfiguration _configuration;
        private readonly JWTSettings _jwtSettings;
        public TokenUtil(IConfiguration _configuration, IOptions<JWTSettings> _jwtSettings)
        {
            this._configuration = _configuration;
            this._jwtSettings = _jwtSettings.Value;
        }

        public string GenerateToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                            new Claim(ClaimTypes.Name, user.FirstName),
                            new Claim(ClaimTypes.Sid, user.Id.ToString()),
                            new Claim(ClaimTypes.Role, user.Role),
                            new Claim("UserId", user.Id.ToString()),
                    }
                    ),
                Expires = DateTime.UtcNow.AddSeconds(2000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string finalToken = tokenHandler.WriteToken(token);
            return finalToken;
        }

    }
}
