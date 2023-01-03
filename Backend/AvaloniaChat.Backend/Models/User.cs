using System;
using System.Collections.Generic;

namespace AvaloniaChat.Backend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public byte[]? Logo { get; set; }

    public string? LastName { get; set; }

    public int? GenderId { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual ICollection<UserGroup> UserGroups { get; } = new List<UserGroup>();
}
