using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Chaos.Permissions;

public class BlogPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var blogGroup = context.AddGroup(BlogPermissions.GroupName, L("Permission:Blog"));

        var blogPermission = blogGroup.AddPermission(BlogPermissions.BlogPosts.Default, L("Permission:Blog"));
        blogPermission.AddChild(BlogPermissions.BlogPosts.Create, L("Permission:Blog.Create"));
        blogPermission.AddChild(BlogPermissions.BlogPosts.Edit, L("Permission:Blog.Edit"));
        blogPermission.AddChild(BlogPermissions.BlogPosts.Delete, L("Permission:Blog.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BlogResource>(name);
    }
}
