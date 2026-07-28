var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithDbGate();
    
var postgresdb = postgres.AddDatabase("postgresdb");

var authLibMigrationService = builder.AddProject<Projects.AuthLib_MigrationService>("authLibMigrationService")
    .WaitFor(postgresdb)
    .WithReference(postgresdb);

var bffMvc = builder.AddProject<Projects.BffMvc>("BffMvc")
    .WaitFor(postgresdb)
    .WithReference(postgresdb);

// After adding all resources, run the app...

builder.Build().Run();
