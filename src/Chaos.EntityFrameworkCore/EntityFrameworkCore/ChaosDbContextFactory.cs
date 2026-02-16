using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Chaos.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ChaosDbContextFactory : IDesignTimeDbContextFactory<ChaosDbContext>
{
    public ChaosDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        ChaosEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<ChaosDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));
        
        return new ChaosDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Chaos.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
