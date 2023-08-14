using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.UserGroup;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvaloniaChat.Backend.Controllers;

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
    [Route("create")]
    public async Task<ActionResult<BaseResponse>> CreateGroup(CreateGroupDto group)
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
                    Message = "Group is null"
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

    [HttpPost]
    [Route("update")]
    public async Task<ActionResult> UpdateGroup(UpdateGroupDto group)
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
                    Message = "Group is null"
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

    [HttpPost]
    [Route("delete")]
    public async Task<ActionResult> DeleteGroup(Guid groupId)
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
                    Message = "Group id is null"
                }
            });
        }
        await _groupService.DeleteGroup(groupId);
        return NoContent();
    }


}

