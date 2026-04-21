using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuillApp.DTOs;
using QuillApp.IServices;
using QuillApp.Mappers;

namespace QuillApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    private bool TryGetCurrentUserId(out int userId)
    {
        userId = default;

        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(idValue, out userId) && userId > 0;
    }

    [HttpPost]
    [ProducesResponseType(typeof(StoryResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<StoryResponseDto>> CreateStory([FromBody] StoryCreateDto dto)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        var entity = dto.ToEntity();
        var created = await _storyService.CreateStoryAsync(entity, currentUserId);

        return CreatedAtAction(
            nameof(GetStory),
            new { storyId = created.StoryId },
            created.ToResponseDto()
        );
    }

    [HttpGet("{storyId:int}")]
    [ProducesResponseType(typeof(StoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public async Task<ActionResult<StoryResponseDto>> GetStory(int storyId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });

        var story = await _storyService.GetStoryAsync(storyId, currentUserId);
        if (story is null)
            return NotFound();
        return Ok(story.ToResponseDto());
    }

    [HttpPut("{storyId:int}")]
    [ProducesResponseType(typeof(StoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<StoryResponseDto>> UpdateStory(int storyId, [FromBody] StoryUpdateDto dto)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        if (dto.StoryId != storyId)
            return BadRequest("Route storyId must match body StoryId.");

        var entity = dto.ToEntity();
        var updated = await _storyService.UpdateStoryAsync(entity, currentUserId);

        if (updated is null) return NotFound();

        return Ok(updated.ToResponseDto());
    }

    [HttpDelete("{storyId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteStory(int storyId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        await _storyService.DeleteStoryAsync(storyId, currentUserId);
        return NoContent();
    }
    
}
