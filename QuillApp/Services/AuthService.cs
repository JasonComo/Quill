using Microsoft.AspNetCore.Identity;
using QuillApp.Models;

namespace QuillApp.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        var user = new ApplicationUser
        {
            Email = email.Trim().ToLowerInvariant(),
            UserName = email.Trim().ToLowerInvariant()
        };

        return await _userManager.CreateAsync(user, password);
    }

    public async Task<SignInResult> LoginAsync(string email, string password)
    {
        var normalized = email.Trim().ToLowerInvariant();
        return await _signInManager.PasswordSignInAsync(
            userName: normalized,
            password: password,
            isPersistent: false,
            lockoutOnFailure: false);
    }
    
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

}