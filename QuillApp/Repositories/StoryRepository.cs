using Microsoft.EntityFrameworkCore;
using QuillApp.Data;
using QuillApp.IRepositories;
using QuillApp.Models;


namespace QuillStory.Repositories;

public class StoryRepository : IStoryRepository
{
    private readonly AppDbContext _context;
    
    public StoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Story> CreateStoryAsync(Story story)
    {
        await _context.Stories.AddAsync(story);
        await _context.SaveChangesAsync();
        return story;
    }

    public async Task<Story?> GetStoryAsync(int storyId, int userId)
    {
        return await _context.Stories.Include(s => s.App)
            .FirstOrDefaultAsync(s =>
                s.StoryId == storyId &&
                s.App != null &&
                s.App.UserId == userId);
        
    }

    public async Task<Story?> UpdateStoryAsync(Story story, int userId)
    {
        var existing = await _context.Stories
            .Include(s => s.App)
            .FirstOrDefaultAsync(s =>
                s.StoryId == story.StoryId &&
                s.App != null &&
                s.App.UserId == userId);
        if (existing == null) return null;

        existing.Title = story.Title;
        existing.Description = story.Description;
        existing.Criteria = story.Criteria;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteStoryAsync(int storyId, int userId)
    {
        var story = await _context.Stories
            .Include(s => s.App)
            .FirstOrDefaultAsync(s =>
                s.StoryId == storyId &&
                s.App != null &&
                s.App.UserId == userId);
        if (story == null)
        {
            return;
        }
        _context.Stories.Remove(story);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> AppBelongsToUserAsync(int appId, int userId)
    {
        return await _context.Apps.AnyAsync(a => a.AppId == appId && a.UserId == userId);
    }
    

}
