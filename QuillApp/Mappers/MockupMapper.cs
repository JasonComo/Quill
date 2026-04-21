using QuillApp.DTOs;
using QuillApp.Models;
using QuillApp.Models.Enums;

namespace QuillApp.Mappers;

public static class MockupMapper
{
    public static Mockup ToEntity(this MockupGenerateDto dto, string htmlDocument) => new()
    {
        StoryId = dto.StoryId,
        HtmlDocument = htmlDocument,
        GenerationPrompt = dto.GenerationPrompt,
        Status = MockupStatus.Ready,
        CreatedAtUtc = DateTime.UtcNow
    };

    public static MockupResponseDto ToResponseDto(this Mockup mockup) => new()
    {
        MockupId = mockup.MockupId,
        StoryId = mockup.StoryId,
        GenerationPrompt = mockup.GenerationPrompt,
        Status = mockup.Status,
        ErrorMessage = mockup.ErrorMessage,
        CreatedAtUtc = mockup.CreatedAtUtc,
        UpdatedAtUtc = mockup.UpdatedAtUtc
    };

    public static MockupDetailResponseDto ToDetailResponseDto(this Mockup mockup) => new()
    {
        MockupId = mockup.MockupId,
        StoryId = mockup.StoryId,
        HtmlDocument = mockup.HtmlDocument,
        GenerationPrompt = mockup.GenerationPrompt,
        Status = mockup.Status,
        ErrorMessage = mockup.ErrorMessage,
        CreatedAtUtc = mockup.CreatedAtUtc,
        UpdatedAtUtc = mockup.UpdatedAtUtc
    };
}
