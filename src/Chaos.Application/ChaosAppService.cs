using Chaos.Localization;
using Volo.Abp.Application.Services;

namespace Chaos;

/* Inherit your application services from this class.
 */
public abstract class ChaosAppService : ApplicationService
{
    protected ChaosAppService()
    {
        LocalizationResource = typeof(ChaosResource);
    }
}
