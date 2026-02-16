using Volo.Abp.Application;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.VirtualFileSystem;

namespace Chaos;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpAspNetCoreSignalRModule),
    typeof(AbpPermissionManagementDomainModule)
)]
public class TodoFeatureModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TodoFeatureModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TodoResource>("en")
                .AddVirtualJson("/Localization/Todo");
        });
    }
}
