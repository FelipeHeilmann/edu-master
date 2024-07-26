using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EduMaster.Application.Abstractions.Service;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EduMaster.Infra.Authentication;

public class JwtService : ITokenService
{
    private JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    } 

    public string GenerateAuthToken(Guid id, string email)
    {
         var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, id.ToString()),
            new(JwtRegisteredClaimNames.Email, email)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(24),
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
