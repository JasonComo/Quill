using QuillApp.DTOs;
using QuillApp.Models;

namespace QuillApp.Mappers;

public static class AppMapper
{
    // Create DTO -> Entity
    public static App ToEntity(this AppCreateDto dto) => new()
    {
        Name = dto.Name,
        Purpose = dto.Purpose,
        AppType = dto.AppType,
        Field = dto.Field,
        TargetUser = dto.TargetUser,
        AdditionalInfo = dto.AdditionalInfo ?? string.Empty,
        
    };
    public static App ToEntity(this AppUpdateDto dto) => new()
    {
        AppId = dto.AppId,
        Name = dto.Name,
        Purpose = dto.Purpose,
        AppType = dto.AppType,
        Field = dto.Field,
        TargetUser = dto.TargetUser,
        AdditionalInfo = dto.AdditionalInfo ?? string.Empty,
        
    };

    // Entity -> Response DTO
    public static AppResponseDto ToResponseDto(this App app) => new()
    {
        AppId = app.AppId,
        Name = app.Name,
        Purpose = app.Purpose,
        AppType = app.AppType,
        Field = app.Field,
        TargetUser = app.TargetUser,
        AdditionalInfo = app.AdditionalInfo,
        UserId = app.UserId
    };
}