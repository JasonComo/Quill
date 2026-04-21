using QuillApp.DTOs;
using QuillApp.Models;

namespace QuillApp.Mappers;

public static class StoryMapper
{
    public static Story ToEntity(this StoryCreateDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        Criteria = dto.Criteria,
        AppId = dto.AppId
    };
    public static Story ToEntity(this StoryUpdateDto dto) => new()
    {
        StoryId = dto.StoryId,
        Title = dto.Title,
        Description = dto.Description,
        Criteria = dto.Criteria,
    };
    // EF entity -> outgoing response DTO
    public static StoryResponseDto ToResponseDto(this Story story) => new()
    {
        StoryId = story.StoryId,
        Title = story.Title,
        Description = story.Description,
        Criteria = story.Criteria,
        AppId = story.AppId
    };
}