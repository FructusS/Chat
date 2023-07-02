using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AvaloniaChat.Domain.Models
{
    public partial class User
    {
        public User()
        {
            UserGroups = new HashSet<UserGroup>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? FirstName { get; set; }
        public byte[]? Logo { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public long? ExpiresIn { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
