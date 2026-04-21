using QuillApp.Models;

namespace QuillApp.IRepositories;

public interface IStoryRepository
{
    Task<Story> CreateStoryAsync(Story Story);
    Task<Story?> GetStoryAsync(int storyId, int userId);
    Task<Story?> UpdateStoryAsync(Story Story, int userId);
    Task DeleteStoryAsync(int storyId, int userId);
    Task<bool> AppBelongsToUserAsync(int appId, int userId);
}
