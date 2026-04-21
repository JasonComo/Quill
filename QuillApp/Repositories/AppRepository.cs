using Microsoft.EntityFrameworkCore;
using QuillApp.Data;
using QuillApp.IRepositories;
using QuillApp.Models;

namespace QuillApp.Repositories;

public class AppRepository : IAppRepository
{
    private readonly AppDbContext _context;

    public AppRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<App> CreateAppAsync(App app)
    {
        await _context.Apps.AddAsync(app);
        await _context.SaveChangesAsync();
        return app;
    }

    public async Task<App?> GetAppAsync(int appId, int userId)
    {
        return await _context.Apps.FirstOrDefaultAsync(a => a.AppId == appId && a.UserId == userId);
        
    }
    
    public async Task<List<App>> GetAppsByUserIdAsync(int userId)
    {
        return await _context.Apps
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<App?> UpdateAppAsync(App app, int userId)
    {
        var existing = await _context.Apps
            .FirstOrDefaultAsync(a => a.AppId == app.AppId && a.UserId == userId);
        if (existing == null) return null;
        
        existing.Name = app.Name;
        existing.Purpose = app.Purpose;
        existing.AdditionalInfo = app.AdditionalInfo;
        existing.TargetUser = app.TargetUser;
        existing.Field = app.Field;
        existing.AppType = app.AppType;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteAppAsync(int appId, int userId)
    {
        var app = await _context.Apps
            .FirstOrDefaultAsync(a => a.AppId == appId && a.UserId == userId);
        if (app == null)
        {
            return;
        }
        _context.Apps.Remove(app);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Story>> GetStoriesByAppIdAsync(int appId, int userId)
    {
        
        var ownsApp = await _context.Apps.AnyAsync(a => a.AppId == appId && a.UserId == userId);
        if (!ownsApp) return new List<Story>();
        return await _context.Stories.Where(s => s.AppId == appId).ToListAsync();
    }
}
