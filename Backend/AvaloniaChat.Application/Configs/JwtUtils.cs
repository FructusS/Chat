using Microsoft.Extensions.Options;

namespace AvaloniaChat.Application.Configs
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
