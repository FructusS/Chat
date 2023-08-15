using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace AvaloniaChat.Infrastructure.Configurations
{
    internal class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder
                .Property(b => b.GroupId)
                .HasValueGenerator<GuidValueGenerator>();
            builder.Property(x => x.GroupTitle).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasData
            (
                new Group
                {
                    GroupId = new Guid("f66475d3-7ea1-422c-b658-353a96732d14"),
                    GroupTitle = "Group 1",
                    UserId = 1
                },
                new Group
                {
                    GroupId = new Guid("0c25b85c-e6cd-4030-90be-090dcc653ddc"),
                    GroupTitle = "Group 2",
                    UserId = 2
                }
            );
        }
    }
}
