using Chaos.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Chaos.Infrastructure;

public static class TodoEfCoreConfiguration
{
    public static void ConfigureTodoFeature(this ModelBuilder builder)
    {
        builder.Entity<Todo>(b =>
        {
            b.ToTable("AppTodos");
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired().HasMaxLength(256);
            b.Property(x => x.Description).HasMaxLength(1024);
            b.Property(x => x.CompleterId);
            b.Property(x => x.CompletionTime);
        });
    }
}
