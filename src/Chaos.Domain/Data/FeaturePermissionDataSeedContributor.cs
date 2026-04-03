using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace Chaos.Data;

public class FeaturePermissionDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IPermissionDataSeeder _permissionDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public FeaturePermissionDataSeedContributor(
        IPermissionDataSeeder permissionDataSeeder,
        ICurrentTenant currentTenant)
    {
        _permissionDataSeeder = permissionDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        var permissions = new[]
        {
            // Todo permissions
            "Chaos.Todos",
            "Chaos.Todos.Create",
            "Chaos.Todos.Edit",
            "Chaos.Todos.Delete",
            // Shopping permissions
            "Chaos.Shopping",
            "Chaos.Shopping.Create",
            "Chaos.Shopping.Edit",
            "Chaos.Shopping.Delete",
            // Blog permissions
            "Chaos.Blog",
            "Chaos.Blog.Create",
            "Chaos.Blog.Edit",
            "Chaos.Blog.Delete"
        };

        await _permissionDataSeeder.SeedAsync(
            RolePermissionValueProvider.ProviderName,
            "admin",
            permissions,
            _currentTenant.Id
        );
    }
}
