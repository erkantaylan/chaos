var builder = DistributedApplication.CreateBuilder(args);

// 1. PostgreSQL database
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume("chaos-postgres-data")
    .PublishAsContainer();

var database = postgres.AddDatabase("Default");

// 2. Run DbMigrator after PostgreSQL is ready
var dbMigrator = builder.AddProject<Projects.Chaos_DbMigrator>("dbmigrator")
    .WithReference(database)
    .WaitFor(database);

// 3. Run API Host after migrations complete (fixed port for Angular compatibility)
builder.AddProject<Projects.Chaos_HttpApi_Host>("api")
    .WithHttpEndpoint(port: 44341)
    .WithReference(database)
    .WaitForCompletion(dbMigrator);

// Note: Angular runs separately via 'npm start' from the angular/ directory
// API: https://localhost:44342 | Angular: http://localhost:4200

builder.Build().Run();
