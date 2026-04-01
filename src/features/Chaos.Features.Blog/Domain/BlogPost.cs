using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Chaos.Domain;

public class BlogPost : FullAuditedAggregateRoot<Guid>
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Summary { get; set; }

    public string? CoverImageUrl { get; set; }

    public BlogStatus Status { get; set; }

    public DateTime? PublishedAt { get; set; }
}
