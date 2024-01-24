using AvaloniaChat.Application.Configs;
using AvaloniaChat.Backend.Business.Services.Interfaces;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace AvaloniaChat.Backend.Middleware
{
    public class JwtMiddleware
    {
        private readonly JwtConfig _jwtConfig;
        private readonly RequestDelegate _next;
        public JwtMiddleware(IOptions<JwtConfig> jwtConfig, RequestDelegate next)
        {
            _next = next;
            _jwtConfig = jwtConfig.Value;
        }
        public async Task Invoke(HttpContext context, IUserService userService, IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var username = authService.ValidateJwtToken(token);
            if (username != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetUserByUsername(username);
            }

            await _next(context);
        }
    }
}
