using Application.Exceptions;
using AvaloniaChat.Application.Configs;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Backend.Business.Services.Interfaces;
using AvaloniaChat.Data.DTO.Auth;
using Microsoft.Extensions.Options;

namespace AvaloniaChat.Backend.Business.Services.Implimentations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthService(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        
        public async Task<AuthResponse> AuthAsync(AuthRequest loginModel)
        {
            var user = await _userService.GetUserByUsername(loginModel.Username);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var accessToken = _jwtService.GenerateAccessToken(loginModel);
            return new AuthResponse
            {
                AccessToken = accessToken,
                UserId = user.UserId
            };
        }
    }
}
