using System;
using Volo.Abp.Application.Dtos;

namespace Chaos.Todos;

public class TodoDto : FullAuditedEntityDto<Guid>
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public Guid? CompletedByUserId { get; set; }

    public string? CompletedByUserName { get; set; }

    public DateTime? CompletedTime { get; set; }

    public string? CreatorUserName { get; set; }
}
