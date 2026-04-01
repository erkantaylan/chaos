namespace Chaos.Permissions;

public static class BlogPermissions
{
    public const string GroupName = "Chaos.Blog";

    public static class BlogPosts
    {
        public const string Default = GroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
