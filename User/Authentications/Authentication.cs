using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace User.Authentications
{
    public class Authentication
    {
        private IConfiguration _config;
        private static string Role = "User";
        public Authentication(IConfiguration configuration) {
            _config = configuration;
        }

        public string GeneratorToken(DTO.User user)
        {
            List<Claim> listClaim = new List<Claim>();
            listClaim.Add(new Claim(ClaimTypes.Role, Role));
            listClaim.Add(new Claim(ClaimTypes.NameIdentifier, user.Username));

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:SecretKey").Value));
            var sc= new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: listClaim,
                signingCredentials: sc,
                expires: DateTime.Now.AddDays(1)
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return  jwt;
        }
    }
}
