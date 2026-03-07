using Volo.Abp.Threading;

namespace Chaos;

public static class ChaosDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
        });
    }
}
