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

// 5. Angular frontend (random port, passed via PORT env var)
var angular = builder.AddJavaScriptApp("angular", "../../angular", runScriptName: "start")
    .WithHttpEndpoint(env: "PORT");

// 6. API Host - receives Angular URL for CORS
var api = builder.AddProject<Projects.Chaos_HttpApi_Host>("api")
    .WithExternalHttpEndpoints()
    .WithReference(database)
    .WithReference(angular)
    .WaitForCompletion(dbMigrator)
    .WaitForCompletion(abpLibs)
    .WithEnvironment("App__AngularUrl", angular.GetEndpoint("http"))
    .WithEnvironment("App__CorsOrigins", angular.GetEndpoint("http"));

// 7. Angular receives API URL
angular
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("services__api__https__0", api.GetEndpoint("https"))
    .WithEnvironment("services__api__http__0", api.GetEndpoint("http"));

builder.Build().Run();
