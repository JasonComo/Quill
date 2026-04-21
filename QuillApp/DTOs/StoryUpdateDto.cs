using System.ComponentModel.DataAnnotations;

namespace QuillApp.DTOs;

public class StoryUpdateDto
{
    [Required]
    public int StoryId { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Criteria { get; set; } = string.Empty;

    
}