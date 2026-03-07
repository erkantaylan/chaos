using Volo.Abp.Threading;

namespace Chaos.EntityFrameworkCore;

public static class ChaosEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        ChaosGlobalFeatureConfigurator.Configure();
        ChaosModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
        });
    }
}
