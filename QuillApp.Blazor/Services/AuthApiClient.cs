using System.Net;
using System.Net.Http.Json;
using QuillApp.Blazor.DTOs;
using QuillApp.Blazor.State;

namespace QuillApp.Blazor.Services;

public class AuthApiClient
{
    private readonly HttpClient _http;
    private readonly CurrentUser _currentUser;

    public AuthApiClient(HttpClient http, CurrentUser currentUser)
    {
        _http = http;
        _currentUser = currentUser;
    }

    public async Task<(bool Succeeded, string? Error)> RegisterAsync(LoginRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", request);
        if (response.IsSuccessStatusCode)
            return (true, null);

        var error = await response.Content.ReadAsStringAsync();
        return (false, string.IsNullOrWhiteSpace(error) ? response.ReasonPhrase : error);
    }

    public async Task<(bool Succeeded, string? Error)> LoginAsync(LoginRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return (false, string.IsNullOrWhiteSpace(error) ? "Invalid email or password." : error);
        }

        return await RefreshCurrentUserAsync();
    }

    public async Task<(bool Succeeded, string? Error)> RefreshCurrentUserAsync()
    {
        var response = await _http.GetAsync("api/auth/me");
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _currentUser.Clear();
            return (false, "Not logged in.");
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return (false, string.IsNullOrWhiteSpace(error) ? response.ReasonPhrase : error);
        }

        var me = await response.Content.ReadFromJsonAsync<AuthMeResponse>();
        if (me is null || !int.TryParse(me.UserId, out var userId))
        {
            _currentUser.Clear();
            return (false, "Login succeeded, but the current user response was invalid.");
        }

        _currentUser.Set(userId, me.Email);
        return (true, null);
    }

    public async Task LogoutAsync()
    {
        await _http.PostAsync("api/auth/logout", content: null);
        _currentUser.Clear();
    }

    private sealed class AuthMeResponse
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
    }
}
