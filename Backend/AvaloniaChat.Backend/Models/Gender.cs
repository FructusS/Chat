using System;
using System.Collections.Generic;

namespace AvaloniaChat.Backend.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string GenderTitle { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
