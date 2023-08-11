using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        //public async Task<List<GroupDto>> GetUserGroup(int userId)
        //{
        //    return await _repository.GetUserGroup(userId);
          
        //}
        public async Task<List<GroupDto>> GetUserGroup(int userId)
        {
            return await _chatDbContext.UserGroups.Where(x => x.UserId == userId).Select(x => new GroupDto()
            {
                GroupId = x.Group.GroupId,
                UserGroupId = x.UsergroupId,
                GroupLogo = x.Group.GroupImage,
                GroupName = x.Group.GroupTitle
            }).ToListAsync();
        }
    }
}
