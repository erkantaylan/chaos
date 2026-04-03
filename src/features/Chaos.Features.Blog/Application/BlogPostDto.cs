using System;
using Chaos.Domain;
using Volo.Abp.Application.Dtos;

namespace Chaos.Application;

public class BlogPostDto : FullAuditedEntityDto<Guid>
{
    public int PostNumber { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Summary { get; set; }

    public string? CoverImageUrl { get; set; }

    public BlogStatus Status { get; set; }

    public DateTime? PublishedAt { get; set; }
}
