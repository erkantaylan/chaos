using Volo.Abp.Modularity;

namespace Chaos;

[DependsOn(
    typeof(ChaosDomainModule),
    typeof(ChaosTestBaseModule)
)]
public class ChaosDomainTestModule : AbpModule
{

}
