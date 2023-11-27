﻿// <auto-generated />
using System;
using AvaloniaChat.Data;
using AvaloniaChat.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AvaloniaChat.Infrastructure.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20230815091123_add_user_admin_id_column_to_group_table")]
    partial class add_user_admin_id_column_to_group_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AvaloniaChat.Domain.Models.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("GroupImage")
                        .HasColumnType("bytea");

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_admin");

                    b.HasKey("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("AvaloniaChat.Domain.Models.Message", b =>
                {
                    b.Property<Guid>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("timestamp(0)")
                        .HasColumnName("SendDate");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("MessageId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("AvaloniaChat.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<long?>("ExpiresIn")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<byte[]>("Logo")
                        .HasColumnType("bytea");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AvaloniaChat.Domain.Models.UserGroup", b =>
                {
                    b.Property<int>("UsergroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UsergroupId"));

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UsergroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("AvaloniaChat.Domain.Models.Group", b =>
                {
                    b.HasOne("AvaloniaChat.Domain.Models.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AvaloniaChat.Domain.Models.Message", b =>
                {
                    b.HasOne("AvaloniaChat.Domain.Models.Group", "Group")
                        .WithMany("Messages")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AvaloniaChat.Domain.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AvaloniaChat.Domain.Models.UserGroup", b =>
                {
                    b.HasOne("AvaloniaChat.Domain.Models.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId");

                    b.HasOne("AvaloniaChat.Domain.Models.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AvaloniaChat.Domain.Models.Group", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("AvaloniaChat.Domain.Models.User", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Messages");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
