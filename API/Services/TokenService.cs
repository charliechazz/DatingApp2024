namespace API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DataEntities;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService  // Se corrigió la sintaxis de la declaración de la clase
{
    private readonly IConfiguration config;  // Se añadió la propiedad para almacenar la configuración

    public TokenService(IConfiguration config)
    {
        this.config = config;
    }

    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new ArgumentException("TokenKey not found");
        if (tokenKey.Length < 64)
        {
            throw new ArgumentException("TokenKey too short");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Agregar el claim "unique_name"
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName),
            new("unique_name", user.UserName)  // Este es el claim que falta
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}