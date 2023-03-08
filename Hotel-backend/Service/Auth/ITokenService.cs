using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

public interface ITokenService
{
    string GenerateRefreshToken();
    string GenerateAccessToken(IEnumerable<Claim> claim);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string refreshToken);
}
