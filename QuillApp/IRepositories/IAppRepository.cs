using QuillApp.Models;
namespace QuillApp.IRepositories {

public interface IAppRepository
{
    Task<App> CreateAppAsync(App app);
    Task<App?> GetAppAsync(int appId, int userId);
    
    Task<List<App>> GetAppsByUserIdAsync(int userId);
    Task<App?> UpdateAppAsync(App app, int userId);
    Task DeleteAppAsync(int appId, int userId);
    
    Task<List<Story>> GetStoriesByAppIdAsync(int appId, int userId);
}}
