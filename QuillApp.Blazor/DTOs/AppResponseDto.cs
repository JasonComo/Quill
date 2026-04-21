namespace QuillApp.Blazor.DTOs;

public class AppResponseDto
{
    public int AppId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;

    public int AppType { get; set; }
    public int Field { get; set; }
    public int TargetUser { get; set; }

    public string? AdditionalInfo { get; set; }
    public int UserId { get; set; }  
}