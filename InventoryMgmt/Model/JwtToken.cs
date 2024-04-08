using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryMgmt.Model
{
    public class JwtToken
    {
        
        private readonly IConfiguration _configuration;

        public JwtToken(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string GeneratedToken(string username, string role)
        {
            // header info
            var algo = SecurityAlgorithms.HmacSha256;

            // payload info
            var claims = new[] {
                new Claim(ClaimTypes.Sid, username),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("Role", role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // signature
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(getAppsettingValue("key")));
            var credentials = new SigningCredentials(securityKey, algo);
            var token = new JwtSecurityToken(getAppsettingValue("issuer"), getAppsettingValue("audience"),
              claims,
              expires: DateTime.Now.AddSeconds(300),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string getAppsettingValue(string value)
        {
            var appSettingValue = _configuration.GetValue<string>($"jwt:{value}");
            return appSettingValue == null ? value : appSettingValue;
        }
        
    }
}
