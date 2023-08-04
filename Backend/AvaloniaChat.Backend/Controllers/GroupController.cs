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
    public async Task<IActionResult> CreateGroup(CreateGroupDto group)
    {
        if (group == null)
        {
            return BadRequest();
        }
       var createdGroup = await _groupService.CreateGroup(group);

       return Ok(createdGroup);
    }

    [HttpPost]
    [Route("update")]
    public async Task UpdateGroup(UpdateGroupDto group)
    {
        await _groupService.UpdateGroup(group);
    }

    [HttpPost]
    [Route("delete")]
    public async Task DeleteGroup(Guid groupId)
    {
        await _groupService.DeleteGroup(groupId);
    }

    [HttpGet]
    [Route("{userId}")]

    public async Task<List<GroupDto>> GetUserGroups(int userId)
    {
        return await _groupService.GetUserGroup(userId);
    }
}

