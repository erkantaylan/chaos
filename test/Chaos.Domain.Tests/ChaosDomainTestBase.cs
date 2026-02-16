using Volo.Abp.Modularity;

namespace Chaos;

/* Inherit from this class for your domain layer tests. */
public abstract class ChaosDomainTestBase<TStartupModule> : ChaosTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
