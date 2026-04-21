using System.ComponentModel.DataAnnotations;

namespace QuillApp.DTOs;

public class UserCreateDto
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}