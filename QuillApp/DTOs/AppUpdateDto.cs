using System.ComponentModel.DataAnnotations;
using QuillApp.Models.Enums;

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
    public AppType AppType { get; set; }

    [Required]
    public Field Field { get; set; }

    [Required]
    public TargetUser TargetUser { get; set; }

    public string? AdditionalInfo { get; set; }
    
}