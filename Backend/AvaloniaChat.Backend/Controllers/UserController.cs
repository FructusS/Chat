using AutoMapper;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvaloniaChat.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost]
    [Route("registraion")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserModel)
    {
        if (createUserModel is null) return BadRequest("Invalid data");

        if (await _userService.GetUserByUsername(createUserModel.Username) != null)
        {
            return BadRequest("User already exist");
        }

        var registraionResponse = await _userService.CreateUser(createUserModel);
        
        return Ok(registraionResponse);
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserModel)
    {
        if (updateUserModel is null) return BadRequest("Invalid data");


        var updateResponse = await _userService.UpdateUser(updateUserModel);

        return Ok(updateResponse);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("delete")]
    public async Task<IActionResult> DeleteUser(string username)
    {
        await _userService.DeleteUser(username);
        return Ok();
    }
}