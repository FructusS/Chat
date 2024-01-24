using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.UserGroup;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Infrastructure.Repository.Interfaces
{
    public interface IGroupRepository
    {
        Task<GroupDto> CreateGroup(CreateGroupDto group);
        Task DeleteGroup(Guid groupId);
        Task<GroupDto> UpdateGroup(UpdateGroupDto group);

        Task<List<GroupDto>> GetUserGroup(int userId);
    }
}
