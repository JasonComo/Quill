using QuillApp.IRepositories;
using QuillApp.IServices;
using QuillApp.Models;

namespace QuillApp.Services;

public class StoryService : IStoryService
{
    private readonly IStoryRepository _storyRepository;

    public StoryService(IStoryRepository StoryRepository)
    {
        _storyRepository = StoryRepository;
      
    }

    public async Task<Story> CreateStoryAsync(Story story, int currentUserId)
    {
        if (story == null)
            throw new ArgumentNullException(nameof(story));
        
        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        story.Title = story.Title?.Trim() ?? string.Empty;
        story.Description = story.Description?.Trim() ?? string.Empty;
        story.Criteria = story.Criteria?.Trim() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(story.Title))
            throw new ArgumentException("Title is required");

        if (string.IsNullOrWhiteSpace(story.Description))
            throw new ArgumentException("Description is required");

        if (string.IsNullOrWhiteSpace(story.Criteria))
            throw new ArgumentException("Criteria is required");
        
        if (story.AppId < 1)
            throw new ArgumentOutOfRangeException(nameof(story.AppId));
        
        var ownsApp = await _storyRepository.AppBelongsToUserAsync(story.AppId, currentUserId);
        if (!ownsApp)
            throw new InvalidOperationException("App not found.");
        
        return await _storyRepository.CreateStoryAsync(story);
    }

    public async Task<Story?> GetStoryAsync(int storyId, int currentUserId)
    {
        if (storyId < 1)
            throw new ArgumentOutOfRangeException(nameof(storyId));

        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        return await _storyRepository.GetStoryAsync(storyId, currentUserId);
    }

    public async Task<Story?> UpdateStoryAsync(Story story, int currentUserId)
    {
        if (story == null)
            throw new ArgumentNullException(nameof(story));

        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        story.Title = story.Title?.Trim() ?? string.Empty;
        story.Description = story.Description?.Trim() ?? string.Empty;
        story.Criteria = story.Criteria?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(story.Title))
            throw new ArgumentException("Title is required");

        if (string.IsNullOrWhiteSpace(story.Description))
            throw new ArgumentException("Description is required");

        if (string.IsNullOrWhiteSpace(story.Criteria))
            throw new ArgumentException("Criteria is required");
        

        return await _storyRepository.UpdateStoryAsync(story, currentUserId);
    }

    public async Task DeleteStoryAsync(int storyId, int currentUserId)
    {
        if (storyId < 1)
            throw new ArgumentOutOfRangeException(nameof(storyId));

        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        await _storyRepository.DeleteStoryAsync(storyId, currentUserId);
    }
    
}