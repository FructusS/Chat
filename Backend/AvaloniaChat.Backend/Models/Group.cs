using System;
using System.Collections.Generic;

namespace AvaloniaChat.Backend.Models;

public partial class Group
{
    
    public int GroupId { get; set; }

    public string GroupTitle { get; set; } = null!;

    public byte[]? GroupImage { get; set; }

    public virtual ICollection<UserGroup> UserGroups { get; } = new List<UserGroup>();
}
