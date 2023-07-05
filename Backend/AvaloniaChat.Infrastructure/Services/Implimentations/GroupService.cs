using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.UserGroup;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Implimentations;
using AvaloniaChat.Infrastructure.Repository.Interfaces;

namespace AvaloniaChat.Infrastructure.Services.Implimentations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;

        public GroupService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<GroupDto> CreateGroup(CreateGroupDto group)
        { 
            return await _repository.CreateGroup(group);
        }


        public async Task DeleteGroup(Guid groupId)
        {
            await _repository.DeleteGroup(groupId);
        }

        public async Task<GroupDto> UpdateGroup(UpdateGroupDto group)
        {
            return await _repository.UpdateGroup(group);
        }

        public async Task<List<GroupDto>> GetUserGroup(int userId)
        {
            return await _repository.GetUserGroup(userId);
        }
    }
}
