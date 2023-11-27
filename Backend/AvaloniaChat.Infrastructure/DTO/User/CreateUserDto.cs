using System.ComponentModel.DataAnnotations;

namespace AvaloniaChat.Application.DTO.User;

public class CreateUserDto
{
    [Required]
    [MaxLength(20)]
    [MinLength(3)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [MinLength(6)]
    public string Password { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [MinLength(6)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}