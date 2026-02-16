var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume("chaos-postgres-data")
    .PublishAsContainer();

var database = postgres.AddDatabase("Default");

// Install ABP client-side libs (login/account MVC pages need them)
var installLibs = builder.AddExecutable("install-libs", "abp", "../Chaos.HttpApi.Host", "install-libs")
    .ExcludeFromManifest();

var angular = builder.AddJavaScriptApp("angular", "../../angular", "start")
    .WithNpm()
    .WithHttpEndpoint(env: "PORT");

var angularUrl = angular.GetEndpoint("http");

// DbMigrator registers OpenIddict clients — needs the Angular URL for redirect URIs
var dbMigrator = builder.AddProject<Projects.Chaos_DbMigrator>("dbmigrator")
    .WithReference(database)
    .WaitFor(database)
    .WithEnvironment("OpenIddict__Applications__Chaos_App__RootUrl", angularUrl);

// API host needs Angular URL for CORS and redirect allowlist
var apiHost = builder.AddProject<Projects.Chaos_HttpApi_Host>("api")
    .WithExternalHttpEndpoints()
    .WithReference(database)
    .WaitForCompletion(dbMigrator)
    .WaitForCompletion(installLibs)
    .WithEnvironment("App__AngularUrl", angularUrl)
    .WithEnvironment("App__CorsOrigins", angularUrl)
    .WithEnvironment("App__RedirectAllowedUrls", angularUrl);

// Angular needs the API host URL for OAuth and API calls
angular
    .WithReference(apiHost)
    .WaitFor(apiHost);

builder.Build().Run();
