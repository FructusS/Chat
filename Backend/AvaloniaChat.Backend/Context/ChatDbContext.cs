using System;
using System.Collections.Generic;
using System.Configuration;
using AvaloniaChat.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Backend.Context;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
        Database.EnsureCreated();
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }



    public virtual DbSet<Group> Groups { get; set; } = null!;
    public virtual DbSet<Message> Messages { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;

}
