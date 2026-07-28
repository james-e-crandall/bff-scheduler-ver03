
namespace AuthLib.Models;

public class Organization
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public virtual ICollection<ApplicationUserOrganization> ApplicationUserOrganizations { get; set; } = new List<ApplicationUserOrganization>();
}
