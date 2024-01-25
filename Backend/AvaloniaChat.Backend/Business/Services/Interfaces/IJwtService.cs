using AvaloniaChat.Application.DTO.Auth;
using System.Security.Claims;

namespace AvaloniaChat.Backend.Business.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(AuthRequest userModel);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string ValidateJwtToken(string jwtToken);

    }
}
