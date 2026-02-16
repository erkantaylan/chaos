using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Chaos.Todos;

public interface ITodoAppService : IApplicationService
{
    Task<PagedResultDto<TodoDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    Task<TodoDto> CreateAsync(CreateTodoDto input);

    Task CompleteAsync(Guid id);
}
