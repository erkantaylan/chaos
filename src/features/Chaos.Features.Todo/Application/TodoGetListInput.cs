using Volo.Abp.Application.Dtos;

namespace Chaos.Application;

public class TodoGetListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
