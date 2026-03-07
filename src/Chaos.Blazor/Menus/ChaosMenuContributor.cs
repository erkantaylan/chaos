using System.Threading.Tasks;
using Chaos.Localization;
using Chaos.MultiTenancy;
using Volo.Abp.UI.Navigation;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.Identity.Blazor;

namespace Chaos.Blazor.Menus;

public class ChaosMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<ChaosResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                ChaosMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 1
            )
        );

        context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                ChaosMenus.Dashboard,
                l["Menu:Dashboard"],
                "/dashboard",
                icon: "fas fa-chart-line",
                order: 2
            )
        );

        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
}
