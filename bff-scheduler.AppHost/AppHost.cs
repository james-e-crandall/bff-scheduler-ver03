var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithDbGate();
    
var postgresdb = postgres.AddDatabase("postgresdb");

var authLibMigrationService = builder.AddProject<Projects.AuthLib_MigrationService>("authLibMigrationService")
    .WaitFor(postgresdb)
    .WithReference(postgresdb);

var schedulerWebsite = builder.AddJavaScriptApp("scheduler-website", "../scheduler-website", runScriptName: "start")
    .WithNpm(installCommand: "ci")
    .WithHttpEndpoint(env: "PORT")
    .PublishAsDockerFile();

var bffMvc = builder.AddProject<Projects.BffMvc>("BffMvc")
    .WaitFor(postgresdb)
    .WithReference(postgresdb)
    .WithReference(schedulerWebsite);

// After adding all resources, run the app...

builder.Build().Run();
