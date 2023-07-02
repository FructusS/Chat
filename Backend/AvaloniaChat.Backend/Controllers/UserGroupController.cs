using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaloniaChat.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserGroupController : ControllerBase
{
    private readonly IUserGroupService _userGroupService;

    public UserGroupController(IUserGroupService userGroupService)
    {
        _userGroupService = userGroupService;
    }

    [HttpPost]
    [Route("adduser")]
    public async Task<UserGroup> AddUserFromGroup([FromQuery] int userId, [FromQuery] Guid groupId)
    {
        return await _userGroupService.AddUserFromGroup(userId, groupId);
    }

    [HttpPost]
    [Route("deleteuser")]
    public async Task DeleteUserFromGroup([FromQuery] int userId, [FromQuery] Guid groupId)
    {
        await _userGroupService.DeleteUserFromGroup(userId, groupId);
    }
}