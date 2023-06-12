using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AvaloniaChat.Backend.Models
{
    public partial class User
    {
        public User()
        {
            UserGroups = new HashSet<UserGroup>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        [JsonIgnore]
        public string PasswordHash { get; set; } = null!;
        public string? FirstName { get; set; }
        public byte[]? Logo { get; set; }
        public string? LastName { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        [JsonIgnore]
        public long? ExpiresIn { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
