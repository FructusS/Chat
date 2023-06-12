using AutoMapper;
using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Interfaces;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvaloniaChat.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
 
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }


        [HttpPost]
        [Route("registraion")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel createUserModel)
        {

            if (createUserModel is null)
            {
                return BadRequest("Invalid data");
            }

            var registraionResponse = await _userService.CreateUser(createUserModel);

            if (registraionResponse == null)
            {
                return NotFound("User already exist");
            }
            else
            {
                return Ok(registraionResponse);
            }
        } 
        [HttpPatch("{userId}")]
   
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserModel updateUserModel)
        {

            if (updateUserModel is null)
            {
                return BadRequest("Invalid data");
            }
            
            var updateResponse = await _userService.UpdateUser(userId, _mapper.Map<User>(updateUserModel));

            if (updateResponse == null)
            {
                return NotFound("User already exist");
            }
            else
            {
                return Ok(updateResponse);
            }
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
}
