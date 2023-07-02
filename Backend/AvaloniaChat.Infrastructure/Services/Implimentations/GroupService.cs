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

        public async Task CreateGroup(Group group)
        { 
            await _repository.CreateGroup(group);
        }

        public async Task DeleteGroup(Group group)
        {
            await _repository.DeleteGroup(group);
        }

        public async Task UpdateGroup(Group group)
        {
           await _repository.UpdateGroup(group);
        }
    }
}
