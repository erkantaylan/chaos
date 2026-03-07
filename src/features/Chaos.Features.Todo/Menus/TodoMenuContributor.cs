using System.Threading.Tasks;
using Chaos.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Chaos.Menus;

public class TodoMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var l = context.GetLocalizer<TodoResource>();

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Chaos.Todos",
                l["Menu:Todos"],
                "/todos",
                icon: "fas fa-list-check",
                order: 2
            ).RequirePermissions(TodoPermissions.Todos.Default)
        );

        return Task.CompletedTask;
    }
}
