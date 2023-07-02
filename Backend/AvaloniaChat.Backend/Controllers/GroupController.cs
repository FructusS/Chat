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
    [Route("create")]
    public async Task CreateGroup(Group group)
    { 
        await _groupService.CreateGroup(group);
    }

    [HttpPost]
    [Route("update")]
    public async Task UpdateGroup(Group group)
    {
        await _groupService.UpdateGroup(group);
    }

    [HttpPost]
    [Route("delete")]
    public async Task DeleteGroup(Group group)
    {
        await _groupService.DeleteGroup(group);
    }
}