using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuillApp.Models;

namespace QuillApp.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<App> Apps => Set<App>();
    public DbSet<Story> Stories => Set<Story>();
    public DbSet<Mockup> Mockups => Set<Mockup>();

}
