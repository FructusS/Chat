using System.Security.Claims;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Data.DTO.Auth;

namespace AvaloniaChat.Backend.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthAsync(AuthRequest loginModel);
    }
}
