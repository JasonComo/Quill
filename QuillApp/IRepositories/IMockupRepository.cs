using QuillApp.Models;

namespace QuillApp.IRepositories;

public interface IMockupRepository
{
    Task<Mockup> CreateMockupAsync(Mockup mockup, int userId);
    Task<Mockup?> GetMockupAsync(int mockupId, int userId);
    Task<List<Mockup>> GetMockupsByStoryIdAsync(int storyId, int userId);
    Task DeleteMockupAsync(int mockupId, int userId);
    Task<bool> StoryBelongsToUserAsync(int storyId, int userId);
}
