using System;
using Chaos.Domain;
using Volo.Abp.Application.Dtos;

namespace Chaos.Application;

public class ProductDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public ProductCategory Category { get; set; }

    public bool IsAvailable { get; set; }
}
