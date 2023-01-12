using AvaloniaChat.Backend.Configs;
using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Models.AuthModels;
using AvaloniaChat.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AvaloniaChat.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly IAuthService _authService;
        private readonly JwtConfig _jwtConfig;
        public AuthController(ChatDbContext userContext, IAuthService tokenService, IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
            _chatDbContext = userContext;
            _authService = tokenService;
        }



        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
            var user = _chatDbContext.Users.FirstOrDefault(x =>
                (x.Username == loginModel.Username));
            if (user is null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
            };
            var accessToken = _authService.GenerateAccessToken(claims);
            return Ok(new AuthResponse { AccessToken = accessToken});
        }

        //[HttpPost]
        //[Route("refresh")]
        //public async Task<IActionResult> Refresh(TokenModel tokenModel)
        //{
        //    if (tokenModel is null)
        //        return BadRequest("Invalid client request");
        //    string accessToken = tokenModel.AccessToken;
        //    string refreshToken = tokenModel.RefreshToken;
        //    var principal = _authService.GetPrincipalFromExpiredToken(accessToken);
        //    var username = principal.Identity.Name; //this is mapped to the Name claim by default
        //    var user = _chatDbContext.Users.SingleOrDefault(u => u.Username == username);
        //    if (user is null || user.RefreshToken != refreshToken || user.ExpiresIn <= DateTime.Now.Ticks)
        //        return BadRequest("Invalid client request");
        //    var newAccessToken = _authService.GenerateAccessToken(principal.Claims);
        //    var newRefreshToken = _authService.GenerateRefreshToken();
        //    user.RefreshToken = newRefreshToken;
        //    await _chatDbContext.SaveChangesAsync();
        //    return Ok(new LoginResponse()
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    });
        //}
        //[HttpPost, Authorize]
        //[Route("revoke")]
        //public async Task<IActionResult> Revoke()
        //{
        //    var username = User.Identity.Name;
        //    var user = _chatDbContext.Users.SingleOrDefault(u => u.Username == username);
        //    if (user == null) return BadRequest();
        //    user.RefreshToken = null;
        //    await _chatDbContext.SaveChangesAsync();
        //    return NoContent();
        //}

    }
}
