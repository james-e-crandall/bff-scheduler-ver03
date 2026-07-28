using Microsoft.AspNetCore.Identity;

namespace AuthLib.Models;

public class ApplicationRole : IdentityRole<string>
{
    // Custom properties added to theAspNetRoles table
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}


