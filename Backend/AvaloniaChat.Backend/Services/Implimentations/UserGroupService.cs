using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Backend.Services.Implimentations
{
    public class UserGroupService : IUserGroupService
    {
        private readonly ChatDbContext _chatDbContext;

        public UserGroupService(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public async Task<UserGroup> AddUserFromGroup(int userId, int groupId)
        {
            var group = new UserGroup { UserId = userId, GroupId = groupId };
            await  _chatDbContext.UserGroups.AddAsync(group);
            await _chatDbContext.SaveChangesAsync();
            return group;
        }

        public async Task DeleteUserFromGroup(int userId, int groupId)
        {
            var useringroup = _chatDbContext.UserGroups.Where(x=> x.UserId == userId && x.GroupId == groupId).First();
            _chatDbContext.UserGroups.Remove(useringroup);
            await _chatDbContext.SaveChangesAsync();
        }
    }
}
