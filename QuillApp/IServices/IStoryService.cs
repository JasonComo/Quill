using QuillApp.Models;

namespace QuillApp.IServices;

public interface IStoryService
{
    Task<Story> CreateStoryAsync(Story Story, int currentUserId);
    Task<Story?> GetStoryAsync(int storyId, int currentUserId);
    Task<Story?> UpdateStoryAsync(Story Story, int currentUserId);
    Task DeleteStoryAsync(int storyId, int currentUserId);
}