using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(CreateUserDto createUser);
        Task<UserDto> GetUser(int userId);
        Task<UserDto> UpdateUser(UpdateUserDto updateUser);
        Task DeleteUser(string username);
        Task<User?> GetUserByUsername(string username);

    }
}
