using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Interfaces;
using AvaloniaChat.Backend.Models;

namespace AvaloniaChat.Backend.Services.Implimentations
{
    public class GroupService : IGroupService
    {
        private readonly ChatDbContext _chatDbContext;

        public GroupService(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public async Task<Group> CreateGroup(Group group)
        {
            _chatDbContext.Groups.Add(group);
            await _chatDbContext.SaveChangesAsync();
            return group;
        }

        public async Task DeleteGroup(Group group)
        {
            _chatDbContext.Groups.Remove(group);
            await _chatDbContext.SaveChangesAsync();
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            _chatDbContext.Groups.Update(group);
            await _chatDbContext.SaveChangesAsync();
            return group;
        }
    }
}
