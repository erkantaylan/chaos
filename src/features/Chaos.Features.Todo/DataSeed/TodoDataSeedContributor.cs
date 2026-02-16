using System;
using System.Threading.Tasks;
using Chaos.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.PermissionManagement;

namespace Chaos.Todos;

public class TodoDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Todo, Guid> _todoRepository;
    private readonly IPermissionDataSeeder _permissionDataSeeder;

    public TodoDataSeedContributor(
        IRepository<Todo, Guid> todoRepository,
        IPermissionDataSeeder permissionDataSeeder)
    {
        _todoRepository = todoRepository;
        _permissionDataSeeder = permissionDataSeeder;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedPermissionsAsync();
        await SeedTodosAsync();
    }

    private async Task SeedPermissionsAsync()
    {
        await _permissionDataSeeder.SeedAsync(
            "R",
            "admin",
            [
                TodoPermissions.Todos.Default,
                TodoPermissions.Todos.Create,
                TodoPermissions.Todos.Edit,
                TodoPermissions.Todos.Delete
            ]
        );
    }

    private async Task SeedTodosAsync()
    {
        if (await _todoRepository.GetCountAsync() > 0)
        {
            return;
        }

        await _todoRepository.InsertAsync(new Todo
        {
            Title = "Set up development environment",
            Description = "Install .NET SDK, Node.js, and configure the database connection"
        });

        await _todoRepository.InsertAsync(new Todo
        {
            Title = "Review ABP module architecture",
            Description = "Study the modular architecture pattern used in this demo"
        });

        await _todoRepository.InsertAsync(new Todo
        {
            Title = "Implement user dashboard",
            Description = "Create a dashboard page showing todo statistics"
        });
    }
}
