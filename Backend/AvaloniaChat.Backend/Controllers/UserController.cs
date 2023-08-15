using AutoMapper;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvaloniaChat.Backend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserModel)
    {
        if (createUserModel is null) return BadRequest(new BaseResponse
        {
            Data = null,
            Error = new ErrorInfoResponse
            {
                ErrorCode = 400,
                Message = "Invalid data"
            },
            Success = false
        });

        if (await _userService.GetUserByUsername(createUserModel.Username) != null)
        {
            return BadRequest(new BaseResponse
            {
                Data = null,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 400,
                    Message = "User already exist"
                },
                Success = false
            });
        }

        var registraionResponse = await _userService.CreateUser(createUserModel);

        return Ok(new BaseResponse
        {
            Data = registraionResponse,
            Error = null,
            Success = true
        });
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserModel)
    {

        if (updateUserModel is null) return BadRequest(new BaseResponse
        {
            Data = null,
            Error = new ErrorInfoResponse
            {
                ErrorCode = 400,
                Message = "Invalid data"
            },
            Success = false
        });


        var updateResponse = await _userService.UpdateUser(updateUserModel);
        return Ok(new BaseResponse
        {
            Data = updateResponse,
            Error = null,
            Success = true
        });

    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteUser(string username)
    {
        if (username == null)
        {
            return BadRequest(new BaseResponse
            {
                Data = null,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 400,
                    Message = "Invalid data"
                },
                Success = false
            });
        }
        await _userService.DeleteUser(username);
        return NoContent();
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        if (userId == null)
        {
            return BadRequest(new BaseResponse
            {
                Data = null,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 400,
                    Message = "Invalid data"
                },
                Success = false
            });
        }

        var user = await _userService.GetUser(userId);

        return Ok(new BaseResponse
        {
            Data = user,
            Error = null,
            Success = true
        });
    }
}