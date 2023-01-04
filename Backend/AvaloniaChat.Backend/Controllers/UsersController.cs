using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Interfaces;
using AvaloniaChat.Backend.Models.Login;
using AvaloniaChat.Backend.Models.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvaloniaChat.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly IUserService _userService;
        public UsersController(ChatDbContext chatDbContext, IUserService userService)
        {
            _chatDbContext = chatDbContext;
            _userService = userService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var loginresponse = await _userService.LoginUser(request);

            if (loginresponse == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            return Ok(loginresponse.Token);
        }


        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult> RegistrationUser([FromBody] RegistrationRequest request)
        {
          
                if (await CheckEmailUser(request.Email))
                {
                    return BadRequest("email is already use");
                } 
                if (await CheckUserName(request.Username))
                {
                    return BadRequest("username already exist");
                }
                var response = await _userService.RegistrationUser(request);
                if (response == null)
                {
                    return BadRequest(new { errorText = "User is exist." });
                }
               
                return Ok();
                
            
        }

        private async Task<bool> CheckEmailUser(string userEmail)
        {
            return await _chatDbContext.Users.AnyAsync(x => x.Email.ToLower() == userEmail.ToLower());
        }  
        private async Task<bool> CheckUserName(string username)
        {
            return await _chatDbContext.Users.AnyAsync(x => x.Username == username);
        }
      
    }
}
