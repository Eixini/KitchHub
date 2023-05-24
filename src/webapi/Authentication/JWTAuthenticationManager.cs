using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace webapi;

public class JwtAuthenticationManager : IJwtAuthenticationManager
{
    private readonly IConfigurationSection _configData;

    public IList<User> Users { get; set; }

    public JwtAuthenticationManager(IConfigurationSection configData)
    {
        this._configData = configData;
        //Console.WriteLine("JWT ctor data: " + configData.GetSection("SecretKey").Value);
        Users = new List<User>();
    }

    public string Authenticate(string email, string password)
    {
        if (!Users.Any(u => u.Email == email && u.Password == password))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configData.GetSection("SecretKey").Value);
        var tokenDescripor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configData.GetSection("Issuer").Value,
            Audience = _configData.GetSection("Audience").Value,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
        };
        var token = tokenHandler.CreateToken(tokenDescripor);

        return tokenHandler.WriteToken(token);
    }


}
