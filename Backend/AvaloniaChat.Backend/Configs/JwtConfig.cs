using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AvaloniaChat.Backend.Configs
{
    public class JwtConfig
    {
        public const string Position = "Jwt";
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int AccessTokenLifeTime { get; set; }
        public int RefreshTokenLifeTime { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
    }
}
