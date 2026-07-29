using System.Diagnostics;
using AuthLib.Data;
using AuthLib.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthLib.MigrationService;

public class Worker(IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{

    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

protected override async Task ExecuteAsync(
        CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity(
            "Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(
        ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(
        ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await dbContext.Database
                .BeginTransactionAsync(cancellationToken);

            var localIds = SeedData.ApplicationRoleList.Select(_x => _x.Id).ToList();

            //ApplicationRoleList
            // Get existing IDs from database
            List<string> existingRoleIds = await dbContext.Roles
                .Where(p => SeedData.ApplicationRoleList.Select(dto => dto.Id).Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync();

            // Get the full objects that are missing by matching against the ID property
            List<ApplicationRole> missingRoles = SeedData.ApplicationRoleList
                .ExceptBy(existingRoleIds, dto => dto.Id)
                .ToList();

            dbContext.Roles.AddRange(missingRoles);

            //OrganizationList
            // Get existing IDs from database
            List<int> existingOrganizationIds = await dbContext.Organizations
                .Where(p => SeedData.OrganizationList.Select(dto => dto.Id).Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync();

            // Get the full objects that are missing by matching against the ID property
            List<Organization> missingOrganization = SeedData.OrganizationList
                .ExceptBy(existingOrganizationIds, dto => dto.Id)
                .ToList();

            dbContext.Organizations.AddRange(missingOrganization);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

}
