using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.UserGroup;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvaloniaChat.Backend.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]

public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupDto group)
    {
        if (group == null)
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
        var createdGroup = await _groupService.CreateGroup(group);

        return Ok(new BaseResponse
        {
            Data = createdGroup,
            Error = null,
            Success = true,
        });
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateGroup(UpdateGroupDto group)
    {
        if (group == null)
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
        var updatedGroup = await _groupService.UpdateGroup(group);

        return Ok(new BaseResponse
        {
            Data = updatedGroup,
            Error = null,
            Success = true,
        });
    }

    [HttpDelete("{groupId}")]
    public async Task<IActionResult> DeleteGroup(Guid groupId)
    {
        if (groupId == null)
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
        await _groupService.DeleteGroup(groupId);
        return NoContent();
    }


}

