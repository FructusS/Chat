using AvaloniaChat.Backend.Interfaces;
using AvaloniaChat.Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaloniaChat.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        [Route ("create")]
        public async Task<Group> CreateGroup(Group group)
        {
            return await _groupService.CreateGroup(group);

        }    
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateGroup(Group group)
        {
            await _groupService.UpdateGroup(group);
            return Ok();
        }    
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteGroup(Group group)
        {
            await _groupService.DeleteGroup(group);
            return Ok();
        }
    }
}
