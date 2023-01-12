using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AvaloniaChat.Backend.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            Messages = new HashSet<Message>();
        }

        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public int UsergroupId { get; set; }
        [JsonIgnore]
        public virtual Group? Group { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
