using AuthLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AuthLib.Configurations;

public class ApplicationUserOrganizationConfiguration : IEntityTypeConfiguration<ApplicationUserOrganization>
{
    public void Configure(EntityTypeBuilder<ApplicationUserOrganization> builder)
    {
        builder.HasOne(_x => _x.ApplicationUser)
            .WithMany(_x => _x.ApplicationUserOrganizations)
            .HasForeignKey(_x => _x.ApplicationUserId);

        builder.HasOne(_x => _x.Organization)
            .WithMany(_x => _x.ApplicationUserOrganizations)
            .HasForeignKey(_x => _x.OrganizationId);
    }
}