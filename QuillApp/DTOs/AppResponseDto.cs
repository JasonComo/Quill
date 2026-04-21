using QuillApp.Models.Enums;

namespace QuillApp.DTOs;

public class AppResponseDto
{
    public int AppId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;

    public AppType AppType { get; set; }
    public Field Field { get; set; }
    public TargetUser TargetUser { get; set; }

    public string? AdditionalInfo { get; set; }

    public int UserId { get; set; }
}