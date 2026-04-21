namespace QuillApp.DTOs;

public class StoryResponseDto
{    
    public int StoryId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Criteria { get; set; } = string.Empty;

    public int AppId { get; set; }
    
}