using System.Security.Claims;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Data.DTO.Auth;

namespace AvaloniaChat.Business.Services.Interfaces
{
    public interface IAuthService
    { 
        string GenerateAccessToken(AuthRequest userModel);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string ValidateJwtToken(string jwtToken);
        Response<AuthResponse> AuthAsync(AuthRequest loginModel);
    }
}
