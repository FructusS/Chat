using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Interfaces;

namespace AvaloniaChat.Infrastructure.Services.Implimentations
{
    public class UserGroupService : IUserGroupService
    {
        private readonly ChatDbContext _chatDbContext;

        public UserGroupService(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public async Task<UserGroup> AddUserFromGroup(int userId, Guid groupId)
        {
            var group = new UserGroup { UserId = userId, GroupId = groupId };
            await  _chatDbContext.UserGroups.AddAsync(group);
            await _chatDbContext.SaveChangesAsync();
            return group;
        }

        public async Task DeleteUserFromGroup(int userId, Guid groupId)
        {
            var useringroup = _chatDbContext.UserGroups.First(x => x.UserId == userId && x.GroupId == groupId);
            _chatDbContext.UserGroups.Remove(useringroup);
            await _chatDbContext.SaveChangesAsync();
        }
    }
}
