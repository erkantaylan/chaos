var builder = DistributedApplication.CreateBuilder(args);

// 1. Spin up a PostgreSQL container via Docker
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume("chaos-postgres-data")
    .PublishAsContainer();

// 2. Create a database named "Default" inside that PostgreSQL instance
var database = postgres.AddDatabase("Default");

// 3. Run the DbMigrator, but only after PostgreSQL is ready
var dbMigrator = builder.AddProject<Projects.Chaos_DbMigrator>("dbmigrator")
    .WithReference(database)
    .WaitFor(database);

// 4. Run the API Host, but only after migrations are done
builder.AddProject<Projects.Chaos_HttpApi_Host>("api")
    .WithExternalHttpEndpoints()
    .WithReference(database)
    .WaitForCompletion(dbMigrator);

// Note: Angular app runs separately via 'npm start' from the angular/ directory
// It will connect to the API at the URL shown in the Aspire Dashboard

builder.Build().Run();
