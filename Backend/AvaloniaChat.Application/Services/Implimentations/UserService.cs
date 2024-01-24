using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Business.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Interfaces;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Infrastructure.Services.Implimentations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<UserDto> CreateUser(CreateUserDto createUser)
        {
            return await _repository.CreateUser(createUser);
        }

        public async Task<UserDto> GetUser(int userId)
        {
            return await _repository.GetUser(userId);
        }


        public async Task<UserDto> UpdateUser(UpdateUserDto updateDataUser)
        {
            //var user = await GetUserById(userId);

            //if (!await CheckUserName(updateDataUser.Username))
            //{
            //    user.Username = updateDataUser.Username;
            //}
            //user.FirstName = updateDataUser.FirstName ?? user.FirstName;
            //user.LastName = updateDataUser.LastName ?? user.LastName;
            //user.Logo = string.IsNullOrEmpty(updateDataUser.Logo.ToString()) ? user.Logo : updateDataUser.Logo;
            //await _chatDbContext.SaveChangesAsync();
            //return user;
            return await _repository.UpdateUser(updateDataUser);
        }

        public async Task DeleteUser(string username)
        {
            await _repository.DeleteUser(username);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _repository.GetUserByUsername(username);   
        }

    }
}
