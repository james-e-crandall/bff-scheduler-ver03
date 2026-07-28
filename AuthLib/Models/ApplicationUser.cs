using Microsoft.AspNetCore.Identity;

namespace AuthLib.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<ApplicationUserOrganization> ApplicationUserOrganizations { get; set; } = new List<ApplicationUserOrganization>();
}


