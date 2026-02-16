using Microsoft.Extensions.Localization;
using Chaos.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Chaos;

[Dependency(ReplaceServices = true)]
public class ChaosBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ChaosResource> _localizer;

    public ChaosBrandingProvider(IStringLocalizer<ChaosResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
