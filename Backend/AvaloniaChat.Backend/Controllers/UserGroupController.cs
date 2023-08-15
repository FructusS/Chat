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
    public async Task<IActionResult> AddUserFromGroup([FromQuery] int userId, [FromQuery] Guid groupId)
    {
        if (userId == null || groupId == null)
        {
            return BadRequest(new BaseResponse
            {
                Success = false,
                Data = null,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 400,
                    Message = "Invalid data"
                }
            });
        }
        var addedUserInGroup = await _userGroupService.AddUserFromGroup(userId, groupId);
        return Ok(new BaseResponse
        {
            Data = new { addedUserInGroup.GroupId, addedUserInGroup.UserId },
            Success = true,
            Error = null
        });
    }

    [HttpDelete("{groupId}/{userId}")]
    public async Task<IActionResult> DeleteUserFromGroup(int userId,Guid groupId)
    {
        if (userId == null || groupId == null)
        {
            return BadRequest(new BaseResponse
            {
                Success = false,
                Data = null,
                Error = new ErrorInfoResponse
                {
                    ErrorCode = 400,
                    Message = "Invalid data"
                }
            });
        }
        await _userGroupService.DeleteUserFromGroup(userId, groupId);
        return NoContent();
    }


    [HttpGet]
    [Route("{userId}")]

    public async Task<IActionResult> GetUserGroups(int userId)
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
                    Message = "Invalid data"
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