using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Infrastructure.Services.Implimentations
{
    public class UserService : IUserService
    {
        private readonly ChatDbContext _chatDbContext;
        public UserService(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;        
        }
        
        public async Task<User> CreateUser(CreateUserDto createUser)
        {
            if (await GetUserByLoginEmail(createUser.Username))
            {
                return null;
            }
           
            User userCreated =  new User
            {
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
           await _chatDbContext.SaveChangesAsync();
        }

        public async Task<User> UpdateUser(int userId,User updateDataUser)
        {
            var user = await GetUserById(userId);

            if (!await CheckUserName(updateDataUser.Username))
            {
                user.Username = updateDataUser.Username;
            }
            user.FirstName = updateDataUser.FirstName ?? user.FirstName;
            user.LastName = updateDataUser.LastName ?? user.LastName;
            user.Logo = string.IsNullOrEmpty(updateDataUser.Logo.ToString()) ? user.Logo : updateDataUser.Logo;
            await _chatDbContext.SaveChangesAsync();
            return user;

        }

        public async Task<bool> GetUserByLoginEmail(string username)
        {
            return await _chatDbContext.Users.AnyAsync(x => x.Username == username);
        }
        public async Task<User> GetUserById(int id)
        {
            return await _chatDbContext.Users.SingleAsync(x => x.UserId == id);
        }
        private async Task<bool> CheckUserName(string username)
        {
            return await _chatDbContext.Users.AnyAsync(x => x.Username == username);
        }
    }
}
