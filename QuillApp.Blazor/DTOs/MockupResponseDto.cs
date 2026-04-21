using QuillApp.Blazor.Models.Enums;

namespace QuillApp.Blazor.DTOs;

public class MockupResponseDto
{
    public int MockupId { get; set; }
    public int StoryId { get; set; }
    public string? GenerationPrompt { get; set; }
    public MockupStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}
