using QuillApp.Models;

namespace QuillApp.IServices;

public interface IMockupService
{
    Task<Mockup> GenerateMockupAsync(int storyId, string? generationPrompt, int currentUserId);
    Task<Mockup?> GetMockupAsync(int mockupId, int currentUserId);
    Task<List<Mockup>> GetMockupsByStoryIdAsync(int storyId, int currentUserId);
    Task DeleteMockupAsync(int mockupId, int currentUserId);
}
