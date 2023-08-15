using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaloniaChat.Infrastructure.Configurations
{
    public class MessagesConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.Property(e => e.SendDate)
                .HasColumnType("timestamp")
                .HasColumnName("SendDate");
            builder.Property(e => e.IsRead)
                .HasDefaultValue(false);
            builder.Property(e => e.MessageText).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.MessageId).IsRequired();
            builder.Property(e => e.GroupId).IsRequired();
            builder.Property(e => e.SendDate).IsRequired();
        }
    }
}
