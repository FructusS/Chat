using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AvaloniaChat.Domain.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            Messages = new HashSet<Message>();
        }

        public int? UserId { get; set; }
        public Guid? GroupId { get; set; }
        public int UsergroupId { get; set; }
        public virtual Group? Group { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
