using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AvaloniaChat.Data.Models;

namespace AvaloniaChat.Domain.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
        }

        public int UserId { get; set; }
        public Guid GroupId { get; set; }
        public int UserGroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
