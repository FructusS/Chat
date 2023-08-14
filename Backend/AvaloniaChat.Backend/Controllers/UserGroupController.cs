using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Implimentations;
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
    [Route("add")]
    public async Task<UserGroup> AddUserFromGroup([FromQuery] int userId, [FromQuery] Guid groupId)
    {
        return await _userGroupService.AddUserFromGroup(userId, groupId);
    }

    [HttpPost]
    [Route("delete")]
    public async Task DeleteUserFromGroup([FromQuery] int userId, [FromQuery] Guid groupId)
    {
        await _userGroupService.DeleteUserFromGroup(userId, groupId);
    }


    [HttpGet]
    [Route("{userId}")]

    public async Task<ActionResult<List<GroupDto>>> GetUserGroups(int userId)
    {
        if (userId == null)
        {
            return BadRequest(new BaseResponse
            {
                Data = null,
                Success = false,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 400,
                    Message = "user id is null"
                }
            });
        }
        var groupList = await _userGroupService.GetUserGroup(userId);
        return Ok(new BaseResponse
        {
            Data = groupList,
            Success = true,
            Error = null
        });
    }

}