using System.Threading.Tasks;
using Chaos.Permissions;
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

        var blogMenu = new ApplicationMenuItem(
            "Chaos.Blog",
            l["Menu:Blog"],
            "/blog",
            icon: "fas fa-blog",
            order: 1
        );

        blogMenu.AddItem(
            new ApplicationMenuItem(
                "Chaos.Blog.Manage",
                l["Menu:ManagePosts"],
                "/blog/manage",
                icon: "fas fa-pen",
                requiredPermissionName: BlogPermissions.BlogPosts.Default
            )
        );

        context.Menu.AddItem(blogMenu);

        return Task.CompletedTask;
    }
}
