using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Backend.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateGroup(Group group);
        Task DeleteGroup(Group group);
        Task UpdateGroup(Group group);
    }
}
