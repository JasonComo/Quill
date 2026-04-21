using System.ComponentModel.DataAnnotations;
using QuillApp.Models.Enums;

namespace QuillApp.Models;

public class Mockup
{
    public int MockupId { get; set; }
    
    [Required]
    public string HtmlDocument { get; set; } = string.Empty;

    public string? GenerationPrompt { get; set; }

    public MockupStatus Status { get; set; } = MockupStatus.Ready;
    public string? ErrorMessage { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
    
    [Required]
    public int StoryId {get; set;}
    public Story? Story { get; set; }
}