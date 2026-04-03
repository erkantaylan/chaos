using Chaos.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Chaos.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ChaosEntityFrameworkCoreModule),
    typeof(ChaosApplicationContractsModule),
    typeof(BlogFeatureModule)
)]
public class ChaosDbMigratorModule : AbpModule
{
}
