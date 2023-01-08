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



    public virtual DbSet<Gender> Genders { get; set; } = null!;
    public virtual DbSet<Group> Groups { get; set; } = null!;
    public virtual DbSet<Message> Messages { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.GenderId).HasColumnName("gender_id");

            entity.Property(e => e.GenderTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender_title");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.Property(e => e.GroupImage)
                .HasColumnType("image")
                .HasColumnName("group_image");

            entity.Property(e => e.GroupTitle)
                .HasMaxLength(50)
                .HasColumnName("group_title");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.Property(e => e.MessageText).HasColumnName("message_text");

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
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");

            entity.Property(e => e.GenderId).HasColumnName("gender_id");

            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");

            entity.Property(e => e.Logo)
                .HasColumnType("image")
                .HasColumnName("logo");

            entity.Property(e => e.PasswordHash)
                .HasColumnType("text")
                .HasColumnName("password_hash");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Gender)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_User_Gender");
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
