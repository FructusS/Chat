using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Data;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Business.Services.Implimentations
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
            var userInGroup = _chatDbContext.UserGroups.First(x => x.UserId == userId && x.GroupId == groupId);
            _chatDbContext.UserGroups.Remove(userInGroup);
            await _chatDbContext.SaveChangesAsync();
        }
        public async Task<List<GroupDto>> GetUserGroup(int userId)
        {
            return await _chatDbContext.UserGroups.Where(x => x.UserId == userId).Select(x => new GroupDto()
            {
                GroupId = x.Group.GroupId,
                UserGroupId = x.UserGroupId,
                GroupLogo = _chatDbContext.UserGroups.Count(x => x.UserId == userId) > 1 ? x.Group.GroupImage : x.User.Logo,
                GroupTitle = x.Group.GroupTitle
            }).ToListAsync();
        }
    }
}
