using Chaos.Domain;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Chaos.Application;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class BlogPostToBlogPostDtoMapper : MapperBase<BlogPost, BlogPostDto>
{
    public override partial BlogPostDto Map(BlogPost source);

    public override partial void Map(BlogPost source, BlogPostDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class CreateUpdateBlogPostDtoToBlogPostMapper : MapperBase<CreateUpdateBlogPostDto, BlogPost>
{
    public override partial BlogPost Map(CreateUpdateBlogPostDto source);

    public override partial void Map(CreateUpdateBlogPostDto source, BlogPost destination);
}
