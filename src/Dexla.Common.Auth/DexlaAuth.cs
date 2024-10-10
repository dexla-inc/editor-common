using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Dexla.Common.Auth;

public class DexlaAuth
{
    public static Claim[] GetClaims(string email, string projectId)
    {
        return
        [
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(ClaimTypes.Name, email),
            new Claim("user_id", email),
            new Claim("project_id", projectId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];
    }

    public static JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims)
    {
        SymmetricSecurityKey key = new(
            Encoding.UTF8.GetBytes(StandardAccessTokenRefreshTokenTest.JWT_TOKEN_SECRET_KEY));
        SigningCredentials signIn = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer: "dexla.ai",
            audience: "dexla.ai",
            claims: claims,
            expires: DateTime.Now.AddDays(31),
            signingCredentials: signIn
        );

        return token;
    }
}