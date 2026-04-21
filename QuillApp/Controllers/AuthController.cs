using Microsoft.AspNetCore.Mvc;
using QuillApp.DTOs;
using QuillApp.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace QuillApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService auth)
    {
        _authService = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserCreateDto dto)
    {
        var result = await _authService.RegisterAsync(dto.Email, dto.Password).ConfigureAwait(false);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var result = await _authService.LoginAsync(dto.Email, dto.Password);

        if (!result.Succeeded)
            return Unauthorized(new { message = "Invalid credentials." });

        return Ok(new { message = "Logged in." });
    }
    
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;

        return Ok(new { userId, email });
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return NoContent();
    }



}