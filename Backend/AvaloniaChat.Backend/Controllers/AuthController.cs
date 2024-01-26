using AvaloniaChat.Application.DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AvaloniaChat.Backend.Attributes;
using AvaloniaChat.Backend.Business.Services.Interfaces;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services;
using AvaloniaChat.Infrastructure.Services.Implimentations;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using AvaloniaChat.Business.Services.Implimentations;
using AvaloniaChat.Data.DTO.Auth;

namespace AvaloniaChat.Backend.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] AuthRequest? loginModel)
    {
        return  await _authService.AuthAsync(loginModel);
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

