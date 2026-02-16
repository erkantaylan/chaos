using Chaos.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Chaos.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ChaosController : AbpControllerBase
{
    protected ChaosController()
    {
        LocalizationResource = typeof(ChaosResource);
    }
}
