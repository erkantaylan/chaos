using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Chaos.Data;
using Volo.Abp.DependencyInjection;

namespace Chaos.EntityFrameworkCore;

public class EntityFrameworkCoreChaosDbSchemaMigrator
    : IChaosDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreChaosDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        await _serviceProvider
            .GetRequiredService<ChaosDbContext>()
            .Database
            .MigrateAsync();
    }
}
