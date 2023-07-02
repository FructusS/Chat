using AvaloniaChat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Infrastructure;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
       
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
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
    }

}
