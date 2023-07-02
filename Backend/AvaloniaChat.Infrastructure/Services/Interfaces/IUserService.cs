using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserDto createUser);
        Task<User> UpdateUser(int userId,User updateUser);
        Task DeleteUser(int id);
    }
}
