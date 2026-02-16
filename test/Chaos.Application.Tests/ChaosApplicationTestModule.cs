using Volo.Abp.Modularity;

namespace Chaos;

[DependsOn(
    typeof(ChaosApplicationModule),
    typeof(ChaosDomainTestModule)
)]
public class ChaosApplicationTestModule : AbpModule
{

}
