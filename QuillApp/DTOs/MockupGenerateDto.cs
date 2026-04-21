using System.ComponentModel.DataAnnotations;
using QuillApp.Models.Enums;

namespace QuillApp.DTOs;

public class MockupGenerateDto
{
    
    [Required]
    public int StoryId { get; set; }

    public string? GenerationPrompt { get; set; }
}