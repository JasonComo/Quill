using QuillApp.IRepositories;
using QuillApp.IServices;
using QuillApp.Models;
using QuillApp.Models.Enums;
using QuillApp.Repositories;

namespace QuillApp.Services;

public class AppService : IAppService
{
    private readonly IAppRepository _appRepository;

    public AppService(IAppRepository AppRepository)
    {
        _appRepository = AppRepository;
    }

    public async Task<App> CreateAppAsync(App app, int currentUserId)
    {
        if (app == null)
            throw new ArgumentNullException(nameof(app));
        
        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));
        
        app.Name = app.Name?.Trim() ?? string.Empty;
        app.Purpose = app.Purpose?.Trim() ?? string.Empty;
        app.AdditionalInfo = app.AdditionalInfo?.Trim() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(app.Name))
            throw new ArgumentException("App name is required");

        if (string.IsNullOrWhiteSpace(app.Purpose))
            throw new ArgumentException("App purpose is required");
        
        if(!Enum.IsDefined(typeof(AppType), app.AppType))
            throw new ArgumentOutOfRangeException(nameof(app.AppType));
 
        if(!Enum.IsDefined(typeof(Field), app.Field))
            throw new ArgumentOutOfRangeException(nameof(app.Field));
        
        if(!Enum.IsDefined(typeof(TargetUser), app.TargetUser))
            throw new ArgumentOutOfRangeException(nameof(app.TargetUser));
        
        app.UserId = currentUserId;

        return await _appRepository.CreateAppAsync(app);
    }

    public async Task<App?> GetAppAsync(int appId, int currentUserId)
    {
        if (appId < 1)
            throw new ArgumentOutOfRangeException(nameof(appId));
        
        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));
        
        return await _appRepository.GetAppAsync(appId, currentUserId);
    }
    
    public async Task<List<App>> GetAppsByUserIdAsync(int currentUserId)
    {
        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        return await _appRepository.GetAppsByUserIdAsync(currentUserId);
    }

    public async Task<App?> UpdateAppAsync(App app, int currentUserId)
    {
        if (app == null)
            throw new ArgumentNullException(nameof(app));
        
        
        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));
        
        app.Name = app.Name?.Trim() ?? string.Empty;
        app.Purpose = app.Purpose?.Trim() ?? string.Empty;
        app.AdditionalInfo = app.AdditionalInfo?.Trim() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(app.Name))
            throw new ArgumentException("App name is required");

        if (string.IsNullOrWhiteSpace(app.Purpose))
            throw new ArgumentException("App purpose is required");
        
        if(!Enum.IsDefined(typeof(AppType), app.AppType))
            throw new ArgumentOutOfRangeException(nameof(app.AppType));
 
        if(!Enum.IsDefined(typeof(Field), app.Field))
            throw new ArgumentOutOfRangeException(nameof(app.Field));
        
        if(!Enum.IsDefined(typeof(TargetUser), app.TargetUser))
            throw new ArgumentOutOfRangeException(nameof(app.TargetUser));
      
        
        app.UserId = currentUserId;
        
        return await _appRepository.UpdateAppAsync(app, currentUserId);
    }

    public async Task DeleteAppAsync(int appId, int currentUserId)
    {
        if (appId < 1)
            throw new ArgumentOutOfRangeException(nameof(appId));
        
        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));
        
        await _appRepository.DeleteAppAsync(appId, currentUserId);
    }

    public async Task<List<Story>> GetStoriesByAppIdAsync(int appId, int currentUserId)
    {
        if (appId < 1)
            throw new ArgumentOutOfRangeException(nameof(appId));

        if (currentUserId < 1)
            throw new ArgumentOutOfRangeException(nameof(currentUserId));

        return await _appRepository.GetStoriesByAppIdAsync(appId, currentUserId);
    }

}
