using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Interfaces;
using AvaloniaChat.Backend.Models.Login;
using AvaloniaChat.Backend.Models.Registration;
using AvaloniaChat.Backend.Models.UserModels;
using AvaloniaChat.Backend.Services.Implimentations;
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


        //[HttpPost]
        //[Route("registration")]
        //public async Task<IActionResult> RegistrationUser([FromBody] CreateUserModel request)
        //{

        //        if (await CheckEmailUser(request.Email))
        //        {
        //            return BadRequest("email is already use");
        //        } 
        //        if (await CheckUserName(request.Username))
        //        {
        //            return BadRequest("username already exist");
        //        }
        //        var response = await _userService.RegistrationUser(request);
        //        if (response == null)
        //        {
        //            return BadRequest(new { errorText = "User is exist." });
        //        }

        //        return Ok(response);


        //}

        //private async Task<bool> CheckEmailUser(string userEmail)
        //{
        //    return await _chatDbContext.Users.AnyAsync(x => x.Email.ToLower() == userEmail.ToLower());
        //}  
        //private async Task<bool> CheckUserName(string username)
        //{
        //    return await _chatDbContext.Users.AnyAsync(x => x.Username == username);
        //}

    }
}
