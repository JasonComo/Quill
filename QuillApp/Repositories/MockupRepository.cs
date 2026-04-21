using Microsoft.EntityFrameworkCore;
using QuillApp.Data;
using QuillApp.IRepositories;
using QuillApp.Models;

namespace QuillApp.Repositories;

public class MockupRepository : IMockupRepository
{
    private readonly AppDbContext _context;

    public MockupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Mockup> CreateMockupAsync(Mockup mockup, int userId)
    {
        var ownsStory = await StoryBelongsToUserAsync(mockup.StoryId, userId);
        if (!ownsStory)
            throw new UnauthorizedAccessException("Story does not belong to the current user.");

        await _context.Mockups.AddAsync(mockup);
        await _context.SaveChangesAsync();
        return mockup;
    }

    public async Task<Mockup?> GetMockupAsync(int mockupId, int userId)
    {
        return await _context.Mockups
            .Include(m => m.Story!)
            .ThenInclude(s => s.App)
            .FirstOrDefaultAsync(m =>
                m.MockupId == mockupId &&
                m.Story != null &&
                m.Story.App != null &&
                m.Story.App.UserId == userId);
    }

    public async Task<List<Mockup>> GetMockupsByStoryIdAsync(int storyId, int userId)
    {
        var ownsStory = await StoryBelongsToUserAsync(storyId, userId);
        if (!ownsStory) return new List<Mockup>();

        return await _context.Mockups
            .Where(m => m.StoryId == storyId)
            .OrderByDescending(m => m.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task DeleteMockupAsync(int mockupId, int userId)
    {
        var mockup = await GetMockupAsync(mockupId, userId);
        if (mockup == null) return;

        _context.Mockups.Remove(mockup);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> StoryBelongsToUserAsync(int storyId, int userId)
    {
        return await _context.Stories
            .Include(s => s.App)
            .AnyAsync(s =>
                s.StoryId == storyId &&
                s.App != null &&
                s.App.UserId == userId);
    }
}
