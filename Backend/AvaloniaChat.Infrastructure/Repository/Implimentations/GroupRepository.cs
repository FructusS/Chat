

using AvaloniaChat.Infrastructure;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Interfaces;

namespace AvaloniaChat.Infrastructure.Repository.Implimentations;

public class GroupRepository : IGroupRepository
{
    private readonly ChatDbContext _context;
    public GroupRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task CreateGroup(Group group)
    {
        await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGroup(Group group)
    {
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGroup(Group group)
    {
        _context.Groups.Update(group);
        await _context.SaveChangesAsync();
    }
}

