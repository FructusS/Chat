using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Application.DTO.User;

public class UserDto
{

    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public byte[]? Logo { get; set; }
    public string? LastName { get; set; }
    public virtual List<UserGroup> UserGroups { get; set; }

}