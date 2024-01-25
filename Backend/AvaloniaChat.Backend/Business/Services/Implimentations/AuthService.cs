using Application.Exceptions;
using AvaloniaChat.Application.Configs;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Backend.Business.Services.Interfaces;
using AvaloniaChat.Data.DTO.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AvaloniaChat.Backend.Business.Services.Implimentations
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IUserService _userService;

        public AuthService(IOptions<JwtConfig> jwtConfig, IUserService userService)
        {
            _userService = userService;
            _jwtConfig = jwtConfig.Value;
        }

        public string GenerateAccessToken(AuthRequest loginModel)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, loginModel.Username),
                new(ClaimsIdentity.DefaultRoleClaimType, "user")
            };
            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.AccessTokenLifeTime),
                signingCredentials: new SigningCredentials(_jwtConfig.SymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
        public string ValidateJwtToken(string token)
        {
            if (token == null)
                throw new NotFoundException("token is null");
      

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _jwtConfig.SymmetricSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = (jwtToken.Claims.First(x => x.Type == "Username").Value);

                // return user id from JWT token if validation successful
                return username;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public async Task<Response<AuthResponse> AuthAsync(AuthRequest loginModel)
        {
            var user = await _userService.GetUserByUsername(loginModel.Username);

            if (user is null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
            {
                return Unauthorized("Username or password is incorrect");

            }

            var accessToken = _authService.GenerateAccessToken(loginModel);
            return Ok(new
            {
                AccessToken = accessToken,
                UserId = user.UserId
            });
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _jwtConfig.SymmetricSecurityKey(),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
