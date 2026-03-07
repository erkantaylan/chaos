using System;
using Volo.Abp.Application.Services;

namespace Chaos.Application;

public interface ITodoAppService : ICrudAppService<TodoDto, Guid, TodoGetListInput, CreateUpdateTodoDto>
{
}
