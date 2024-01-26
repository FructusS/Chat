using Application.Exceptions;
using AvaloniaChat.Application.Configs;
using AvaloniaChat.Backend.Business.Services.Interfaces;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
        public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token.IsNullOrEmpty())
            {
                throw new NotFoundException("Token not found");
            }

            var username = jwtService.ValidateJwtToken(token);
            context.Items["User"] = userService.GetUserByUsername(username);
            await _next(context);
        }
    }
}
