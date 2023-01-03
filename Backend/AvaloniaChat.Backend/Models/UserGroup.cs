using System;
using System.Collections.Generic;

namespace AvaloniaChat.Backend.Models;

public partial class UserGroup
{
    public int? UserId { get; set; }

    public int? GroupId { get; set; }

    public string UsergroupId { get; set; } = null!;

    public virtual Group? Group { get; set; }

    public virtual ICollection<Message> Messages { get; } = new List<Message>();

    public virtual User? User { get; set; }
}
