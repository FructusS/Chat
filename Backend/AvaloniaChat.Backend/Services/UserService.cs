using AvaloniaChat.Backend.Configs;
using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Interfaces;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Models.Login;
using AvaloniaChat.Backend.Models.Registration;
using AvaloniaChat.Backend.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AvaloniaChat.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly IConfiguration _config;
        public UserService(ChatDbContext chatDbContext, IConfiguration config)
        {
            _chatDbContext = chatDbContext;
            _config = config;
        }
        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            var user = _chatDbContext.Users.FirstOrDefault(x => x.Username == loginRequest.Username);
            if (user == null ||  BCrypt.Net.BCrypt.Verify(loginRequest.Username,user.PasswordHash))
            {
                return null;
            }
            string? token = GenerateToken(user);
            return new LoginResponse { Token = token };
        }

        public async Task<RegistrationResponse> RegistrationUser(RegistrationRequest registrationRequest)
        {
            if (_chatDbContext.Users.FirstOrDefaultAsync(x=> x.Username == registrationRequest.Username) != null)
            {
                return null;
            }

            User user = new User
            {
                Username = registrationRequest.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrationRequest.Password)
            };
            _chatDbContext.Users.Add(user);
            _chatDbContext.SaveChanges();
            return new RegistrationResponse { UserName = user.Username, Email = user.Email };
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username) ,
                new Claim("Email",user.Email)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);



            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Desktop"],
                    claims: claimsIdentity.Claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
