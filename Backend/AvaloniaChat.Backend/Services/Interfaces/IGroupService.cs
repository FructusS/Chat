using AvaloniaChat.Backend.Models;

namespace AvaloniaChat.Backend.Interfaces
{
    public interface IGroupService
    {
        Task<Group> CreateGroup(Group group);
        Task DeleteGroup(Group group);
        Task<Group> UpdateGroup(Group group);
    }
}
