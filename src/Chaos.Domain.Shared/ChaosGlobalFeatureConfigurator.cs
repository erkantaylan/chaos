using Volo.Abp.GlobalFeatures;
using Volo.Abp.Threading;

namespace Chaos;

public static class ChaosGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
        });
    }
}
