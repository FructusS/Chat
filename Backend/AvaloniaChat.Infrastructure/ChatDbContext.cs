using AvaloniaChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace AvaloniaChat.Infrastructure;

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
        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property(e => e.SendDate)
                .HasColumnType("timestamp(0)")
                .HasColumnName("SendDate");
        });
        modelBuilder
            .Entity<Group>()
            .Property(b => b.GroupId)
            .HasValueGenerator<GuidValueGenerator>();
    }

}
