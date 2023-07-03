using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Application.DTO.User;

public class UserDto
{
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public byte[]? Logo { get; set; }
    public string? LastName { get; set; }
}