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

// 5. Angular frontend - define first so API can reference it for CORS
var angular = builder.AddJavaScriptApp("angular", "../../angular", runScriptName: "start")
    .WithHttpEndpoint(port: 4200, env: "PORT");

// 6. Run the API Host with dynamic Angular URL for CORS
var api = builder.AddProject<Projects.Chaos_HttpApi_Host>("api")
    .WithExternalHttpEndpoints()
    .WithReference(database)
    .WaitForCompletion(dbMigrator)
    .WaitForCompletion(abpLibs)
    .WithEnvironment("App__AngularUrl", angular.GetEndpoint("http"))
    .WithEnvironment("App__CorsOrigins", angular.GetEndpoint("http"));

// 7. Angular waits for API and gets API URL
angular
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("API_URL", api.GetEndpoint("https"));

builder.Build().Run();
