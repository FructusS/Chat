using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaloniaChat.Infrastructure.Configurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("UserGroups");
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.GroupId).IsRequired();
            builder.Property(x => x.UserGroupId).IsRequired();



            builder.HasData
            (
                new UserGroup
                {
                    GroupId = new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"),
                    UserId = 1,
                    UserGroupId = 1
                }, new UserGroup
                {
                    GroupId = new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"),
                    UserId = 2,
                    UserGroupId = 2
                }, new UserGroup
                {
                    GroupId = new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"),
                    UserId = 3,
                    UserGroupId = 3
                }, new UserGroup
                {
                    GroupId = new Guid("f66475d3-7ea1-422c-b658-353a96732d14"),
                    UserId = 1,
                    UserGroupId = 4
                }
            );
        }
    }
}
