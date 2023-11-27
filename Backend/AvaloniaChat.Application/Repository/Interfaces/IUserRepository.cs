using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Infrastructure.Repository.Interfaces;

public interface IUserRepository
{
    Task<UserDto> CreateUser(CreateUserDto createUser);
    Task<UserDto> UpdateUser(UpdateUserDto updateUser);
    Task<UserDto> GetUser(int userId);
    Task<User?> GetUserByUsername(string username);
    Task DeleteUser(string username);
    Task<bool> UserIsExistByUsername(string username);
}