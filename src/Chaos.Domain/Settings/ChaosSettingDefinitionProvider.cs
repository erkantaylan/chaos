using Volo.Abp.Settings;

namespace Chaos.Settings;

public class ChaosSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ChaosSettings.MySetting1));
    }
}
