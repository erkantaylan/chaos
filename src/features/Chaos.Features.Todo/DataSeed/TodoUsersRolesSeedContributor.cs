using System.Threading.Tasks;
using Chaos.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace Chaos.Todos;

/// <summary>
/// Seeds test users and roles for Todo permission testing.
/// </summary>
public class TodoUsersRolesSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IdentityUserManager _userManager;
    private readonly IdentityRoleManager _roleManager;
    private readonly IPermissionDataSeeder _permissionDataSeeder;
    private readonly IGuidGenerator _guidGenerator;

    // Role names
    public const string TodoViewerRole = "TodoViewer";
    public const string TodoEditorRole = "TodoEditor";
    public const string TodoAdminRole = "TodoAdmin";

    // Test user names
    public const string TestUser1 = "testuser1";
    public const string TestUser2 = "testuser2";
    public const string TestUser3 = "testuser3";

    // Default password for test users
    private const string DefaultPassword = "1q2w3E*";

    public TodoUsersRolesSeedContributor(
        IdentityUserManager userManager,
        IdentityRoleManager roleManager,
        IPermissionDataSeeder permissionDataSeeder,
        IGuidGenerator guidGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _permissionDataSeeder = permissionDataSeeder;
        _guidGenerator = guidGenerator;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedRolesAsync();
        await SeedUsersAsync();
    }

    private async Task SeedRolesAsync()
    {
        // TodoViewer - read only (Default permission only)
        await CreateRoleIfNotExistsAsync(TodoViewerRole);
        await _permissionDataSeeder.SeedAsync(
            "R",
            TodoViewerRole,
            [TodoPermissions.Todos.Default]
        );

        // TodoEditor - CRUD permissions (Default + Create + Edit)
        await CreateRoleIfNotExistsAsync(TodoEditorRole);
        await _permissionDataSeeder.SeedAsync(
            "R",
            TodoEditorRole,
            [
                TodoPermissions.Todos.Default,
                TodoPermissions.Todos.Create,
                TodoPermissions.Todos.Edit
            ]
        );

        // TodoAdmin - full access (all permissions)
        await CreateRoleIfNotExistsAsync(TodoAdminRole);
        await _permissionDataSeeder.SeedAsync(
            "R",
            TodoAdminRole,
            [
                TodoPermissions.Todos.Default,
                TodoPermissions.Todos.Create,
                TodoPermissions.Todos.Edit,
                TodoPermissions.Todos.Delete
            ]
        );
    }

    private async Task CreateRoleIfNotExistsAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role != null)
        {
            return;
        }

        role = new IdentityRole(_guidGenerator.Create(), roleName);
        await _roleManager.CreateAsync(role);
    }

    private async Task SeedUsersAsync()
    {
        // testuser1 - TodoViewer (read only)
        await CreateUserWithRoleAsync(
            TestUser1,
            $"{TestUser1}@test.local",
            DefaultPassword,
            TodoViewerRole);

        // testuser2 - TodoEditor (can create and edit)
        await CreateUserWithRoleAsync(
            TestUser2,
            $"{TestUser2}@test.local",
            DefaultPassword,
            TodoEditorRole);

        // testuser3 - TodoAdmin (full access)
        await CreateUserWithRoleAsync(
            TestUser3,
            $"{TestUser3}@test.local",
            DefaultPassword,
            TodoAdminRole);
    }

    private async Task CreateUserWithRoleAsync(
        string userName,
        string email,
        string password,
        string roleName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user != null)
        {
            // User exists, ensure they have the role
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return;
        }

        user = new IdentityUser(
            _guidGenerator.Create(),
            userName,
            email);

        await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, roleName);
    }
}
