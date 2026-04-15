using CollectionDomain.Dtos.Users;
using CollectionDomain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollectionDomain.Services;

public class TokenService : ITokenService
{
    public async Task<string> GenerateToken(UserDataLoginDto userDataLoginDto, string key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var keyVault = key;

        ArgumentNullException.ThrowIfNull(keyVault);

        var keyDefault = Encoding.ASCII.GetBytes(keyVault);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
           {
                    new Claim("email", userDataLoginDto.User.Email),
                    new Claim("id", userDataLoginDto.User._id),
                    new Claim("name", userDataLoginDto.User.Name),
           }),
            Expires = DateTime.UtcNow.AddHours(2),
            //Issuer = "http://13.59.37.186:5011",
            Issuer = "http://localhost:5011",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDefault), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
