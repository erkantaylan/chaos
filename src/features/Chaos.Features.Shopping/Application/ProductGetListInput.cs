using Volo.Abp.Application.Dtos;

namespace Chaos.Application;

public class ProductGetListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
