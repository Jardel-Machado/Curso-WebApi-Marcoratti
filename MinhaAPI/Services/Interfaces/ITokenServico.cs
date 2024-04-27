using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalogo.Services.Interfaces;

public interface ITokenServico
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration config);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config);
}
