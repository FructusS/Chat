using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AvaloniaChat.Backend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;
    [JsonIgnore]
    public string PasswordHash { get; set; } = null!;
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public byte[]? Logo { get; set; }
    public string? LastName { get; set; }
    [JsonIgnore]
    public int? GenderId { get; set; }
    [JsonIgnore]
    public virtual Gender? Gender { get; set; }
    [JsonIgnore]
    public virtual ICollection<UserGroup> UserGroups { get; set; }
}
