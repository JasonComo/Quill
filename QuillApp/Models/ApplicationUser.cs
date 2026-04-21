using Microsoft.AspNetCore.Identity;

namespace QuillApp.Models;

public class ApplicationUser : IdentityUser<int>
{
    public int UserId => Id;

    
}