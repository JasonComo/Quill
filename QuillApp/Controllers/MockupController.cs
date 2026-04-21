using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuillApp.DTOs;
using QuillApp.IServices;
using QuillApp.Mappers;

namespace QuillApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MockupController : ControllerBase
{
    private readonly IMockupService _mockupService;

    public MockupController(IMockupService mockupService)
    {
        _mockupService = mockupService;
    }

    private bool TryGetCurrentUserId(out int userId)
    {
        userId = default;

        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(idValue, out userId) && userId > 0;
    }

    [HttpPost("story/{storyId:int}/generate")]
    [ProducesResponseType(typeof(MockupResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MockupResponseDto>> GenerateMockup(
        int storyId,
        [FromBody] MockupGenerateDto dto)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });

        if (dto.StoryId != storyId)
            return BadRequest("Route storyId must match body StoryId.");

        var created = await _mockupService.GenerateMockupAsync(
            storyId,
            dto.GenerationPrompt,
            currentUserId);

        return CreatedAtAction(
            nameof(GetMockup),
            new { mockupId = created.MockupId },
            created.ToResponseDto()
        );
    }

    [HttpGet("story/{storyId:int}")]
    [ProducesResponseType(typeof(List<MockupResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<MockupResponseDto>>> GetMockupsByStory(int storyId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });

        var mockups = await _mockupService.GetMockupsByStoryIdAsync(storyId, currentUserId);
        return Ok(mockups.Select(m => m.ToResponseDto()).ToList());
    }

    [HttpGet("{mockupId:int}")]
    [ProducesResponseType(typeof(MockupDetailResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MockupDetailResponseDto>> GetMockup(int mockupId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });

        var mockup = await _mockupService.GetMockupAsync(mockupId, currentUserId);
        if (mockup is null) return NotFound();

        return Ok(mockup.ToDetailResponseDto());
    }

    [HttpGet("{mockupId:int}/download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DownloadMockup(int mockupId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });

        var mockup = await _mockupService.GetMockupAsync(mockupId, currentUserId);
        if (mockup is null) return NotFound();

        var fileName = $"mockup-{mockup.MockupId}.html";
        var bytes = Encoding.UTF8.GetBytes(mockup.HtmlDocument);

        return File(bytes, "text/html; charset=utf-8", fileName);
    }

    [HttpDelete("{mockupId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteMockup(int mockupId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });

        await _mockupService.DeleteMockupAsync(mockupId, currentUserId);
        return NoContent();
    }
}
