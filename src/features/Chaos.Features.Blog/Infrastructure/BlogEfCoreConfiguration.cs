using Chaos.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Chaos.Infrastructure;

public static class BlogEfCoreConfiguration
{
    public static void ConfigureBlogFeature(this ModelBuilder builder)
    {
        builder.Entity<BlogPost>(b =>
        {
            b.ToTable("AppBlogPosts");
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired().HasMaxLength(256);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(256);
            b.Property(x => x.Content).IsRequired();
            b.Property(x => x.Summary).HasMaxLength(512);
            b.Property(x => x.CoverImageUrl).HasMaxLength(512);
            b.HasIndex(x => x.Slug).IsUnique();
        });
    }
}
