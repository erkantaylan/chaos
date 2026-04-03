using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Chaos.Application;

public interface IBlogPostAppService : ICrudAppService<BlogPostDto, Guid, BlogPostGetListInput, CreateUpdateBlogPostDto>
{
    Task<PagedResultDto<BlogPostDto>> GetPublishedListAsync(BlogPostGetListInput input);
    Task<BlogPostDto?> GetByPostNumberAsync(int postNumber);
}
