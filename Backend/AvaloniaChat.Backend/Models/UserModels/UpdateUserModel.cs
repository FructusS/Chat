using System.ComponentModel.DataAnnotations;
namespace AvaloniaChat.Backend.Models.UserModels
{
    public class UpdateUserModel
    {
        public string Username { get; set; } = null!;
        public string? FirstName { get; set; }
        public byte[]? Logo { get; set; }
        public string? LastName { get; set; }
    }
}
