using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaloniaChat.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasData
            (
                new User
                {
                    UserId = 1,
                    Username = "test1",
                   PasswordHash = BCrypt.Net.BCrypt.HashPassword("test1"),
                   
                },
                new User
                {
                    UserId = 2,
                    Username = "test2",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("test2"),

                },
                new User
                {
                    UserId = 3,
                    Username = "test3",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("test3"),

                }
            );
        }
    }
}
