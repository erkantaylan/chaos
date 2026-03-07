using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Chaos.Data;

public class NullChaosDbSchemaMigrator : IChaosDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
