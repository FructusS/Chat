using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Models.UserModels;

namespace AvaloniaChat.Backend.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserModel createUser);
        Task<User> UpdateUser(int userId,User updateUser);
        Task DeleteUser(int id);
    }
}
