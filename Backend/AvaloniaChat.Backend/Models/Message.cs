using System;
using System.Collections.Generic;

namespace AvaloniaChat.Backend.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int UsergroupId { get; set; }
        public string MessageText { get; set; } = null!;

        public virtual UserGroup Usergroup { get; set; } = null!;
    }
}
