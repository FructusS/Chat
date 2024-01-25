using AvaloniaChat.Data.Entities;
using AvaloniaChat.Data.Models;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Data;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {

    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {

    }


    public virtual DbSet<Group> Groups { get; set; } = null!;
    public virtual DbSet<Message> Messages { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new UserGroupConfiguration());
        modelBuilder.ApplyConfiguration(new MessagesConfiguration());
    }

}

