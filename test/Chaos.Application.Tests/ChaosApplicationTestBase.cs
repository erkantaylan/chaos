using Volo.Abp.Modularity;

namespace Chaos;

public abstract class ChaosApplicationTestBase<TStartupModule> : ChaosTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
