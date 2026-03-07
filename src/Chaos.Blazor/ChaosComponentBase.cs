using Chaos.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Chaos.Blazor;

public abstract class ChaosComponentBase : AbpComponentBase
{
    protected ChaosComponentBase()
    {
        LocalizationResource = typeof(ChaosResource);
    }
}
