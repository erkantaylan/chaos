using System.Threading.Tasks;

namespace Chaos.Data;

public interface IChaosDbSchemaMigrator
{
    Task MigrateAsync();
}
