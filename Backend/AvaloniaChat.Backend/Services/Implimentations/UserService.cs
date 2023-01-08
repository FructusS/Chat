using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Interfaces;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Backend.Services.Implimentations
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
        
        public async Task<User> CreateUser(CreateUserModel createUser)
        {
            if (await GetUserByLoginEmail(createUser.Username, createUser.Email))
            {
                return null;
            }
           
            User userCreated =  new User
            {
                Email = createUser.Email,
                Username = createUser.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUser.Password)
            }; 
            await  _chatDbContext.Users.AddAsync(userCreated);
            await _chatDbContext.SaveChangesAsync();
            return userCreated;

        }

        public async Task DeleteUser(int id)
        {
            _chatDbContext.Users.Remove( await GetUserById(id));
            _chatDbContext.SaveChanges();
        }

        public Task<User> UpdateUser(UpdateUserModel createUser)
        {
            return null;
        }



        public async Task<bool> GetUserByLoginEmail(string username,string email)
        {
            return await _chatDbContext.Users.AnyAsync(x => x.Username == username && x.Email == email);
        }
        public async Task<User> GetUserById(int id)
        {
            return await _chatDbContext.Users.SingleAsync(x => x.UserId == id);
        }

        //public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        //{
        //    var user = _chatDbContext.Users.FirstOrDefault(x => x.Username == loginRequest.Username);
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        //    {
        //        return null;
        //    }
        //    string? token = GenerateToken(user);
        //    return new LoginResponse { Token = token };
        //}

        //public async Task<RegistrationResponse> RegistrationUser(CreateUserRequest registrationRequest)
        //{
        //    if (_chatDbContext.Users.FirstOrDefault(x => x.Username == registrationRequest.Username) != null)
        //    {
        //        return null;
        //    }

        //    User user = new User
        //    {
        //        Username = registrationRequest.Username,
        //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrationRequest.Password)
        //    };
        //    _chatDbContext.Users.Add(user);
        //    _chatDbContext.SaveChanges();
        //    return new RegistrationResponse { UserName = user.Username, Email = user.Email };
        //}
        //public string GenerateToken(User user)
        //{
        //    var claims = new List<Claim> {
        //        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username)

        //    };
        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);



        //    // создаем JWT-токен
        //    var jwt = new JwtSecurityToken(
        //            issuer: _config["Jwt:Issuer"],
        //            audience: _config["Jwt:Audience"],
        //            claims: claimsIdentity.Claims,
        //            expires: DateTime.Now.AddMinutes(30),
        //            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha256));
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        //    return encodedJwt;
        //}
    }
}
