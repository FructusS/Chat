using AvaloniaChat.Backend.Configs;
using AvaloniaChat.Backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AvaloniaChat.Backend.utils
{
    public class JwtUtils
    {
        private readonly JwtConfig _jwtConfig;
        public JwtUtils(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }
       
    }
}
