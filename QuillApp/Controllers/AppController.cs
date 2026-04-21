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
public class AppController : ControllerBase
{
    private readonly IAppService _appService;

    public AppController(IAppService appService)
    {
        _appService = appService;
    }

    private bool TryGetCurrentUserId(out int userId)
    {
        userId = default;

        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(idValue, out userId) && userId > 0;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AppResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AppResponseDto>> CreateApp([FromBody] AppCreateDto dto)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        
        var entity = dto.ToEntity();

        var created = await _appService.CreateAppAsync(entity, currentUserId);

        return CreatedAtAction(
            nameof(GetApp),
            new { appId = created.AppId },
            created.ToResponseDto()
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AppResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<AppResponseDto>>> GetApps()
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        
        var apps = await _appService.GetAppsByUserIdAsync(currentUserId);
        return Ok(apps.Select(a => a.ToResponseDto()).ToList());
    }

    [HttpGet("{appId:int}")]
    [ProducesResponseType(typeof(AppResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AppResponseDto>> GetApp(int appId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        
        var app = await _appService.GetAppAsync(appId, currentUserId);
        if (app == null) return NotFound();
        return Ok(app.ToResponseDto());
    }

    [HttpPut("{appId:int}")]
    [ProducesResponseType(typeof(AppResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AppResponseDto>> UpdateApp(int appId, [FromBody] AppUpdateDto dto)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        if (dto.AppId != appId)
            return BadRequest("Route appId must match body AppId.");

        var entity = dto.ToEntity();

        var updated = await _appService.UpdateAppAsync(entity, currentUserId);
        if (updated is null) return NotFound();

        return Ok(updated.ToResponseDto());
    }

    [HttpDelete("{appId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteApp(int appId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        
        await _appService.DeleteAppAsync(appId, currentUserId);
        return NoContent();
    }

    [HttpGet("{appId:int}/stories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetStories(int appId)
    {
        if (!TryGetCurrentUserId(out var currentUserId))
            return Unauthorized(new { message = "Invalid user session." });
        var stories = await _appService.GetStoriesByAppIdAsync(appId, currentUserId);
        var response = stories.Select(s => s.ToResponseDto()).ToList();
        return Ok(response);
    }
}
