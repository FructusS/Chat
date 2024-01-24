using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Backend.Business.Services.Interfaces;
using AvaloniaChat.Data.Entities;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Interfaces;

namespace AvaloniaChat.Backend.Business.Services.Implimentations
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
            var user = await _repository.GetUser(userId);
            if (user == null)
            {
                throw new NotFoundException($"{nameof(User)} not found");
            }
            return user;
        }


        public async Task<UserDto> UpdateUser(UpdateUserDto updateDataUser)
        {
            return await _repository.UpdateUser(updateDataUser);
        }

        public async Task DeleteUser(string username)
        {
            await _repository.DeleteUser(username);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var user = await _repository.GetUserByUsername(username);
            if (user == null)
            {
                throw new NotFoundException($"{nameof(User)} not found");
            }
            return user;
        }

    }
}
