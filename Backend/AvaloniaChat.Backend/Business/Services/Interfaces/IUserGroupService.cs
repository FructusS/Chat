﻿using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Infrastructure.Services.Interfaces
{
    public interface IUserGroupService
    {
        Task<UserGroup> AddUserFromGroup(int userId, Guid groupId);
        Task DeleteUserFromGroup(int userId, Guid groupId);
        Task<List<GroupDto>> GetUserGroup(int userId);

    }
}
