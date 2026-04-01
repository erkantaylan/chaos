using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Chaos.Menus;

public class BlogMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var l = context.GetLocalizer<BlogResource>();

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Chaos.Blog",
                l["Menu:Blog"],
                "/blog",
                icon: "fas fa-blog",
                order: 1
            )
        );

        return Task.CompletedTask;
    }
}
