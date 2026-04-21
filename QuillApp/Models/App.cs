using QuillApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuillApp.Models;

public class App
{
    public int AppId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Purpose { get; set; } = string.Empty;
    
    [Required]
    public AppType AppType { get; set; }
    
    [Required]
    public Field Field { get; set; }
    
    [Required]
    public TargetUser TargetUser { get; set; }
    
    public string AdditionalInfo { get; set; } = string.Empty;
    
    [Required]
    public int UserId { get; set; }
    
    public ApplicationUser? User { get; set; }
    public List<Story> Stories { get; set; } = new();

}
