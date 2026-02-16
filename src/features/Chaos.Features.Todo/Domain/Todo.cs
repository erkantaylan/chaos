using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Chaos.Todos;

public class Todo : FullAuditedAggregateRoot<Guid>
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public Guid? CompletedByUserId { get; set; }

    public DateTime? CompletedTime { get; set; }
}
