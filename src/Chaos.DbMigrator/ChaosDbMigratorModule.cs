using Chaos.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Chaos.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ChaosEntityFrameworkCoreModule),
    typeof(ChaosApplicationContractsModule)
)]
public class ChaosDbMigratorModule : AbpModule
{
}
