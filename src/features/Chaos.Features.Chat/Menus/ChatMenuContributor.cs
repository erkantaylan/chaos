using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Chaos.Menus;

public class ChatMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var l = context.GetLocalizer<ChatResource>();

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Chaos.Chat",
                l["Menu:Chat"],
                "/chat",
                icon: "fas fa-comments",
                order: 3
            )
        );

        return Task.CompletedTask;
    }
}
