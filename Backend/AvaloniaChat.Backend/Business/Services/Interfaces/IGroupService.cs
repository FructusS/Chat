using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.UserGroup;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Backend.Services.Interfaces
{
    public interface IGroupService
    {
        Task<GroupDto> CreateGroup(CreateGroupDto group);
        Task DeleteGroup(Guid groupId);
        Task<GroupDto> UpdateGroup(UpdateGroupDto group);

    }
}
