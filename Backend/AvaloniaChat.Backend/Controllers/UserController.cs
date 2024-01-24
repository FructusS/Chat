using AutoMapper;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Backend.Business.Services.Interfaces;
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
        if (createUserModel is null)
            return BadRequest("Invalid data");

        if (await _userService.GetUserByUsername(createUserModel.Username) != null)
        {
            return BadRequest("User already exist");
        }

        var registrationResponse = await _userService.CreateUser(createUserModel);


        return Ok(registrationResponse);
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserModel)
    {

        if (updateUserModel is null)
            return BadRequest("Invalid data");


        var updateUser = await _userService.UpdateUser(updateUserModel);
        return Ok(updateUser);

    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteUser(string username)
    {
        if (username == null)
        {
            return BadRequest("Invalid data");
        }
        await _userService.DeleteUser(username);
        return NoContent();
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {

        if (userId == null)
        {
            return BadRequest("Invalid data");
        }
        var user = await _userService.GetUser(userId);
        return Ok(user);
    }
}