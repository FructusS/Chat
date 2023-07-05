using System.Security.Claims;
using AvaloniaChat.Application.Configs;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Infrastructure;
using AvaloniaChat.Infrastructure.Services.Implimentations;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AvaloniaChat.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest loginModel)
    {
        if (loginModel is null) return BadRequest("Invalid client request");
        var user = await _userService.GetUserByUsername(loginModel.Username);

        if (user == null) return BadRequest("User not found");

        if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
            return Unauthorized();

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, loginModel.Username),
            new(ClaimsIdentity.DefaultRoleClaimType, "user")
        };
        var accessToken = _authService.GenerateAccessToken(claims);
        return Ok(new AuthResponse { AccessToken = accessToken, UserId = user.UserId});
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