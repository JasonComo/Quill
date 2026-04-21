using QuillApp.Models;

namespace QuillApp.IServices;

public interface IAppService
{
    Task<App> CreateAppAsync(App App, int currentUserId);
    Task<App?> GetAppAsync(int appId, int currentUserId);
    Task<App?> UpdateAppAsync(App App, int currentUserId);
    Task DeleteAppAsync(int appId, int currentUserId);
    Task<List<App>> GetAppsByUserIdAsync(int currentUserId);
    Task<List<Story>> GetStoriesByAppIdAsync(int appId, int currentUserId);
}