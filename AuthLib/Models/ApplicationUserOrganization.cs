
namespace AuthLib.Models;

public class ApplicationUserOrganization
{
    public int Id { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public required ApplicationUser ApplicationUser { get; set; }
    public required string ApplicationUserId { get; set; }
    public required Organization Organization { get; set;}
    public required int OrganizationId { get; set; }
    
}
