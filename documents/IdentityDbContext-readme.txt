https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.identitypasskeydata?view=aspnetcore-10.0

This exception occurs because Entity Framework Core treats IdentityPasskeyData as a standalone entity rather than an owned JSON property.
Introduced in ASP.NET Core Identity for .NET 10, passkey support uses an owned entity mapping (b.OwnsOne(p => p.Data).ToJson())
 to store passkey information as JSON within the AspNetUserPasskeys table.
  If you override OnModelCreating in your DbContext but forget to invoke the base configuration,
  EF Core attempts to map IdentityPasskeyData as a standard relational table, which fails because the class lacks an Id primary key property.

How to Fix the Error

  1. Add base.OnModelCreating(modelBuilder)

  The most common fix is ensuring that the parent configuration of IdentityDbContext executes properly.
   Ensure that base.OnModelCreating(modelBuilder) is the very first line inside your overridden OnModelCreating method.

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 🔴 CRITICAL: This must be called first to wire up Identity Passkeys correctly!
        base.OnModelCreating(modelBuilder); 

        // Your custom entity configurations go below here
    }
}

--------------------------
This error occurs because your application is looking for UserManager<ApplicationUser> via dependency injection, but ASP.NET Core Identity was configured using a custom user class (e.g., ApplicationUser or MyUser).

The Cause

If you configured Identity in Program.cs or Startup.cs using a custom class like this:

builder.Services.AddDefaultIdentity<ApplicationUser>() // Custom class used here

ASP.NET Core only registers UserManager<ApplicationUser>. It does not register UserManager<ApplicationUser>. If any file in your project asks for the default IdentityUser variant, the application crashes.

How to Fix It1. Check your Razor Views (Most Common)Scaffolded identity pages and default layout files often have a hardcoded reference to IdentityUser. Search your entire project for IdentityUser and look specifically in _LoginPartial.cshtml or _ManageNav.cshtml.Change the injected types at the top of those files to match your custom user class:

@inject SignInManager<ApplicationUser> SignInManager<ApplicationUser>
@inject UserManager<ApplicationUser> UserManager<ApplicationUser>

SignInManager
to
SignInManager<ApplicationUser>

UserManager<ApplicationUser>
to
UserManager<ApplicationUser>
------------