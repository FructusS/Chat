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
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IUserService _userService;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _mapper = mapper;
        _userService = userService;
    }


    [HttpPost]
    [Route("registraion")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserModel)
    {
        if (createUserModel is null) return BadRequest("Invalid data");

        var registraionResponse = await _userService.CreateUser(createUserModel);

        if (registraionResponse == null)
            return NotFound("User already exist");
        return Ok(registraionResponse);
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserModel)
    {
        if (updateUserModel is null) return BadRequest("Invalid data");

        var updateResponse = await _userService.UpdateUser(userId, _mapper.Map<User>(updateUserModel));

        if (updateResponse == null)
            return NotFound("User already exist");
        return Ok(updateResponse);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("delete")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return Ok();
    }
}