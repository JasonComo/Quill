using System.ComponentModel.DataAnnotations;

namespace QuillApp.DTOs;

public class StoryCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Criteria { get; set; } = string.Empty;

    [Required]
    public int AppId { get; set; }

}