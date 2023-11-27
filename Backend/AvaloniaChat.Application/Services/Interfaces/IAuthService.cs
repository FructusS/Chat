using System.Security.Claims;
using AvaloniaChat.Application.DTO.Auth;

namespace AvaloniaChat.Backend.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateAccessToken(AuthRequest userModel);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string ValidateJwtToken(string jwtToken);
    }
}
