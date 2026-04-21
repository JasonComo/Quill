using QuillApp.Models;

namespace QuillApp.IServices;

public interface IAiMockupGenerator
{
    Task<string> GenerateHtmlMockupAsync(Story story, string? generationPrompt);
}
