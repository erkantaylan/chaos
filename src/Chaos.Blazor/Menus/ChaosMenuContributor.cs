using System.Threading.Tasks;
using Chaos.Localization;
using Chaos.MultiTenancy;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;
using Volo.Abp.UI.Navigation;

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
            administration.AddItem(
                new ApplicationMenuItem(
                    ChaosMenus.TenantManagement,
                    "Tenants",
                    "/tenant-management/tenants",
                    icon: "fas fa-building",
                    order: 1
                ).RequirePermissions(TenantManagementPermissions.Tenants.Default)
            );
        }

        var identityMenu = new ApplicationMenuItem(
            ChaosMenus.Identity,
            "Identity",
            icon: "fas fa-id-card",
            order: 2
        );

        identityMenu.AddItem(
            new ApplicationMenuItem(
                ChaosMenus.IdentityUsers,
                "Users",
                "/identity/users",
                icon: "fas fa-users"
            ).RequirePermissions(IdentityPermissions.Users.Default)
        );

        identityMenu.AddItem(
            new ApplicationMenuItem(
                ChaosMenus.IdentityRoles,
                "Roles",
                "/identity/roles",
                icon: "fas fa-user-shield"
            ).RequirePermissions(IdentityPermissions.Roles.Default)
        );

        administration.AddItem(identityMenu);

        return Task.CompletedTask;
    }
}
