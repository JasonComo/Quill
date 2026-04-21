using System.ComponentModel.DataAnnotations;

namespace QuillApp.DTOs;

public class AppUpdateDto
{
    [Required]
    public int AppId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    public int AppType { get; set; }

    [Required]
    public int Field { get; set; }

    [Required]
    public int TargetUser { get; set; }

    public string? AdditionalInfo { get; set; }

    [Required]
    public int UserId { get; set; }
}