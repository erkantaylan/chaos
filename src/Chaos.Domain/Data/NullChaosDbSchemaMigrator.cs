using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Chaos.Data;

/* This is used if database provider does't define
 * IChaosDbSchemaMigrator implementation.
 */
public class NullChaosDbSchemaMigrator : IChaosDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
