using AuthLib.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthLib.Configurations;

public static class SeedData
{
    public static ApplicationRole ApplicationRoleOne = new ApplicationRole { 
        Id = "1", 
        Name = "Test", 
        NormalizedName = "TEST" 
    };

    public static ICollection<ApplicationRole> ApplicationRoleList = [ ApplicationRoleOne ];

    public static Organization OrganizationOne = new Organization { 
        Id = 1, 
        Name = "Test",
    };

    public static ICollection<Organization> OrganizationList = [ OrganizationOne ];

}