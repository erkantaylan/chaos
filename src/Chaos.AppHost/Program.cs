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

// 4. Install ABP client-side libraries for the API Host
var abpLibs = builder.AddExecutable("abp-install-libs", "abp", "../Chaos.HttpApi.Host", "install-libs")
    .ExcludeFromManifest();

// 5. Run the API Host, but only after migrations and ABP libs are done
var api = builder.AddProject<Projects.Chaos_HttpApi_Host>("api")
    .WithExternalHttpEndpoints()
    .WithReference(database)
    .WaitForCompletion(dbMigrator)
    .WaitForCompletion(abpLibs);

// 6. Run the Angular frontend, but only after the API is ready
builder.AddJavaScriptApp("angular", "../../angular", runScriptName: "start")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT");

builder.Build().Run();
