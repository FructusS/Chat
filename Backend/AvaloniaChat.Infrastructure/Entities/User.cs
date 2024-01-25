using AvaloniaChat.Data.Models;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Data.Entities
{
    public partial class User
    {
        public User()
        {

        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? FirstName { get; set; }
        public byte[]? Logo { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public long? ExpiresIn { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
        public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
