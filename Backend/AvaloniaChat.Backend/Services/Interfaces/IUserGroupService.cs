using AvaloniaChat.Backend.Models;

namespace AvaloniaChat.Backend.Services.Interfaces
{
    public interface IUserGroupService
    {
        Task<UserGroup> AddUserFromGroup(int userId,int groupId);
        Task DeleteUserFromGroup(int userId, int groupId);
       
    }
}
