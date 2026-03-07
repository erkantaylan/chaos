using System;
using Volo.Abp.Application.Services;

namespace Chaos.Application;

public interface IProductAppService : ICrudAppService<ProductDto, Guid, ProductGetListInput, CreateUpdateProductDto>
{
}
