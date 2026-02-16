using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaos.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Chaos.Todos;

[Authorize(TodoPermissions.Todos.Default)]
public class TodoAppService : ApplicationService, ITodoAppService
{
    private readonly IRepository<Todo, Guid> _todoRepository;
    private readonly IIdentityUserRepository _userRepository;
    private readonly IHubContext<TodoHub> _hubContext;

    public TodoAppService(
        IRepository<Todo, Guid> todoRepository,
        IIdentityUserRepository userRepository,
        IHubContext<TodoHub> hubContext)
    {
        _todoRepository = todoRepository;
        _userRepository = userRepository;
        _hubContext = hubContext;
    }

    public async Task<PagedResultDto<TodoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _todoRepository.GetQueryableAsync();

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        queryable = queryable.OrderByDescending(t => t.CreationTime);
        queryable = queryable.Skip(input.SkipCount).Take(input.MaxResultCount);

        var todos = await AsyncExecuter.ToListAsync(queryable);
        var dtos = todos.Select(MapToDto).ToList();

        var userIds = todos
            .SelectMany(t => new[] { t.CreatorId, t.CompletedByUserId })
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .Distinct()
            .ToList();

        if (userIds.Count > 0)
        {
            var users = await _userRepository.GetListByIdsAsync(userIds);
            var userDict = users.ToDictionary(u => u.Id, u => u.UserName);

            foreach (var dto in dtos)
            {
                if (dto.CreatorId.HasValue && userDict.TryGetValue(dto.CreatorId.Value, out var creatorName))
                {
                    dto.CreatorUserName = creatorName;
                }

                if (dto.CompletedByUserId.HasValue && userDict.TryGetValue(dto.CompletedByUserId.Value, out var completerName))
                {
                    dto.CompletedByUserName = completerName;
                }
            }
        }

        return new PagedResultDto<TodoDto>(totalCount, dtos);
    }

    [Authorize(TodoPermissions.Todos.Create)]
    public async Task<TodoDto> CreateAsync(CreateTodoDto input)
    {
        var todo = new Todo
        {
            Title = input.Title,
            Description = input.Description
        };

        await _todoRepository.InsertAsync(todo);

        var dto = MapToDto(todo);

        await _hubContext.Clients.All.SendAsync("TodoCreated", dto);

        return dto;
    }

    public async Task CompleteAsync(Guid id)
    {
        var todo = await _todoRepository.GetAsync(id);
        todo.IsCompleted = true;
        todo.CompletedByUserId = CurrentUser.Id;
        todo.CompletedTime = Clock.Now;

        await _todoRepository.UpdateAsync(todo);

        await _hubContext.Clients.All.SendAsync("TodoCompleted", id);
    }

    private static TodoDto MapToDto(Todo todo)
    {
        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            CompletedByUserId = todo.CompletedByUserId,
            CompletedTime = todo.CompletedTime,
            CreationTime = todo.CreationTime,
            CreatorId = todo.CreatorId,
            LastModificationTime = todo.LastModificationTime,
            LastModifierId = todo.LastModifierId
        };
    }
}
