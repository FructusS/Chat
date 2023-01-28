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
        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.Property(e => e.GroupImage).HasColumnName("group_image");

            entity.Property(e => e.GroupTitle)
                .HasMaxLength(50)
                .HasColumnName("group_title");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.Property(e => e.MessageText).HasColumnName("message_text");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.Property(e => e.UsergroupId).HasColumnName("usergroup_id");

            entity.HasOne(d => d.Usergroup)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.UsergroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_UserGroup");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.Property(e => e.ExpiresIn).HasColumnName("expires_in");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");

            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");

            entity.Property(e => e.Logo).HasColumnName("logo");

            entity.Property(e => e.PasswordHash)
                .HasColumnType("text")
                .HasColumnName("password_hash");

            entity.Property(e => e.RefreshToken)
                .IsUnicode(false)
                .HasColumnName("refresh_token");

            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.ToTable("UserGroup");

            entity.Property(e => e.UsergroupId).HasColumnName("usergroup_id");

            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Group)
                .WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_UserGroup_Group");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserGroup_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
