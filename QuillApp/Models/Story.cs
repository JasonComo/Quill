using System.ComponentModel.DataAnnotations;

namespace QuillApp.Models;

public class Story
{
    public int StoryId { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string Criteria {get; set;} = string.Empty;
    
    [Required]
    public int AppId {get; set;}
    public App? App { get; set; }
    public List<Mockup> Mockups { get; set; } = new();
    
}
