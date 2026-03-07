using System.Threading.Tasks;
using Chaos.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Chaos.Menus;

public class ShoppingMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var l = context.GetLocalizer<ShoppingResource>();

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Chaos.Shopping",
                l["Menu:Shopping"],
                "/shopping",
                icon: "fas fa-shopping-cart",
                order: 3
            ).RequirePermissions(ShoppingPermissions.Products.Default)
        );

        return Task.CompletedTask;
    }
}
