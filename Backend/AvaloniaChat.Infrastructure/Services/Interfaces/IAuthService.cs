using System.Security.Claims;

namespace AvaloniaChat.Backend.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
