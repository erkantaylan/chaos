using System;
using System.ComponentModel.DataAnnotations;
using Chaos.Domain;

namespace Chaos.Application;

public class CreateUpdateBlogPostDto
{
    [Required]
    [StringLength(256)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(256)]
    public string Slug { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    [StringLength(512)]
    public string? Summary { get; set; }

    [StringLength(512)]
    public string? CoverImageUrl { get; set; }

    public BlogStatus Status { get; set; }

    public DateTime? PublishedAt { get; set; }
}
