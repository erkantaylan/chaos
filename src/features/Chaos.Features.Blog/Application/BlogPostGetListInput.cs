using Volo.Abp.Application.Dtos;

namespace Chaos.Application;

public class BlogPostGetListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
