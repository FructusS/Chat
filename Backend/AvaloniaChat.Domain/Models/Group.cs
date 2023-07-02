using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AvaloniaChat.Domain.Models
{
    public partial class Group
    {
        public Group()
        {
            UserGroups = new HashSet<UserGroup>();
        }

        public Guid GroupId { get; set; }
        public string GroupTitle { get; set; } = null!;
        public byte[]? GroupImage { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
