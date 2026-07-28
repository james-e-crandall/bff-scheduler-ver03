using AuthLib.Data;
using AuthLib.MigrationService;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb"), sqlOptions =>
        sqlOptions.MigrationsAssembly("AuthLib.MigrationService")
    ));
builder.EnrichNpgsqlDbContext<ApplicationDbContext>();

var host = builder.Build();
host.Run();
