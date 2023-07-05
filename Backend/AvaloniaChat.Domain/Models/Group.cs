using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AvaloniaChat.Domain.Models
{
    public partial class Group
    {
        public Group()
        {
           // Messages = new HashSet<Message>();
        }

        public Guid GroupId { get; set; }
        public string GroupTitle { get; set; } = null!;
        public byte[]? GroupImage { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
