using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace webapi;

public class JWTAuthenticationManager : IJWTAuthenticationManager
{
    private readonly string _tokenKey;

    public IList<User> Users { get; set; }

    public JWTAuthenticationManager(string tokenKey)
    {
        this._tokenKey = tokenKey;
        Users = new List<User>();
    }

    public string Authenticate(string email, string password)
    {
        if (!Users.Any(u => u.Email == email && u.Password == password))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(email);
        var tokenDescripor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescripor);

        return tokenHandler.WriteToken(token);
    }


}
