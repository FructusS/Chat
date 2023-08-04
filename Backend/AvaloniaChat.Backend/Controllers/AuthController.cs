using AvaloniaChat.Application.DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services;
using AvaloniaChat.Infrastructure.Services.Implimentations;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

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
    public async Task<ActionResult<BaseResponse>> Login([FromBody] AuthRequest loginModel)
    {
        if (loginModel is null)
            return Unauthorized(new BaseResponse
            {
                Success = false,
                Data = null,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 400,
                    Message = "Invalid user"
                }
            });

        var user = await _userService.GetUserByUsername(loginModel.Username);

        if (user == null)
        {
            return Unauthorized(new BaseResponse
            {
                Success = false,
                Data = null,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 401,
                    Message = "Username or password is incorrect"
                }
            });

        }

        if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
            return Unauthorized(new BaseResponse
            {
                Success = false,
                Data = null,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 401,
                    Message = "Username or password is incorrect"
                }
            });


        var accessToken = _authService.GenerateAccessToken(loginModel);
        return Ok(new BaseResponse
        {
            Success = true,
            Data = new
            {
                AccessToken = accessToken,
                UserId = user.UserId
            },
            Error = null
        });
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