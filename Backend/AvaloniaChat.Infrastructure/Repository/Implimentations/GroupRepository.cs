

using AutoMapper;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.UserGroup;
using AvaloniaChat.Infrastructure;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Infrastructure.Repository.Implimentations;

public class GroupRepository : IGroupRepository
{
    private readonly ChatDbContext _context;

    private readonly IMapper _mapper;
    public GroupRepository(ChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GroupDto> CreateGroup(CreateGroupDto createGroupDto)
    {
        var group = _mapper.Map<Group>(createGroupDto);
        var createdGroup = await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync();
        return _mapper.Map<GroupDto>(createdGroup.Entity);
    }

    public async Task DeleteGroup(Guid groupId)
    {
        var group = await GetGroupById(groupId);
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }

    public async Task<GroupDto> UpdateGroup(UpdateGroupDto updateGroupDto)
    {
        var group = await GetGroupById(updateGroupDto.GroupId);
        var updatedGroup = await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync();
        return _mapper.Map<GroupDto>(updatedGroup.Entity);
    }

    public async Task<List<GroupDto>> GetUserGroup(int userId)
    {
        return await _context.UserGroups.Where(x => x.UserId == userId).Select(x => new GroupDto()
        {
            GroupId = x.Group.GroupId,
            UserGroupId = x.UserGroupId,
            GroupLogo = x.Group.GroupImage,
            GroupTitle = x.Group.GroupTitle
        }).ToListAsync();
    }

    private async Task<Group> GetGroupById(Guid id)
    {
        return await _context.Groups.FindAsync(id);

    }
}

