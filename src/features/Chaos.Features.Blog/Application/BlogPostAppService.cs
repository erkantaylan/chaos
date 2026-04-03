using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaos.Domain;
using Chaos.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Chaos.Application;

public class BlogPostAppService
    : CrudAppService<BlogPost, BlogPostDto, Guid, BlogPostGetListInput, CreateUpdateBlogPostDto>,
      IBlogPostAppService
{
    public BlogPostAppService(IRepository<BlogPost, Guid> repository)
        : base(repository)
    {
        GetPolicyName = BlogPermissions.BlogPosts.Default;
        GetListPolicyName = BlogPermissions.BlogPosts.Default;
        CreatePolicyName = BlogPermissions.BlogPosts.Create;
        UpdatePolicyName = BlogPermissions.BlogPosts.Edit;
        DeletePolicyName = BlogPermissions.BlogPosts.Delete;
    }

    public override async Task<BlogPostDto> CreateAsync(CreateUpdateBlogPostDto input)
    {
        if (input.Status == BlogStatus.Published && input.PublishedAt == null)
        {
            input.PublishedAt = DateTime.UtcNow;
        }

        return await base.CreateAsync(input);
    }

    public override async Task<BlogPostDto> UpdateAsync(Guid id, CreateUpdateBlogPostDto input)
    {
        var entity = await Repository.GetAsync(id);

        if (input.Status == BlogStatus.Published && entity.Status != BlogStatus.Published && input.PublishedAt == null)
        {
            input.PublishedAt = DateTime.UtcNow;
        }

        MapToEntity(input, entity);
        await Repository.UpdateAsync(entity, autoSave: true);

        return await MapToGetOutputDtoAsync(entity);
    }

    public override async Task<PagedResultDto<BlogPostDto>> GetListAsync(BlogPostGetListInput input)
    {
        var queryable = await Repository.GetQueryableAsync();

        if (!string.IsNullOrWhiteSpace(input.Filter))
        {
            queryable = queryable.Where(p =>
                p.Title.Contains(input.Filter) || (p.Summary != null && p.Summary.Contains(input.Filter)));
        }

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        if (!string.IsNullOrWhiteSpace(input.Sorting))
        {
            queryable = ApplySorting(queryable, input);
        }
        else
        {
            queryable = queryable.OrderByDescending(p => p.CreationTime);
        }

        queryable = ApplyPaging(queryable, input);

        var posts = await AsyncExecuter.ToListAsync(queryable);
        var dtos = ObjectMapper.Map<List<BlogPost>, List<BlogPostDto>>(posts);

        return new PagedResultDto<BlogPostDto>(totalCount, dtos);
    }

    [AllowAnonymous]
    public async Task<BlogPostDto?> GetByPostNumberAsync(int postNumber)
    {
        var queryable = await Repository.GetQueryableAsync();
        var post = await AsyncExecuter.FirstOrDefaultAsync(
            queryable.Where(p => p.PostNumber == postNumber && p.Status == BlogStatus.Published));
        return post == null ? null : ObjectMapper.Map<BlogPost, BlogPostDto>(post);
    }

    [AllowAnonymous]
    public async Task<PagedResultDto<BlogPostDto>> GetPublishedListAsync(BlogPostGetListInput input)
    {
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable.Where(p => p.Status == BlogStatus.Published);

        if (!string.IsNullOrWhiteSpace(input.Filter))
        {
            queryable = queryable.Where(p =>
                p.Title.Contains(input.Filter) || (p.Summary != null && p.Summary.Contains(input.Filter)));
        }

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        queryable = queryable.OrderByDescending(p => p.PublishedAt);
        queryable = ApplyPaging(queryable, input);

        var posts = await AsyncExecuter.ToListAsync(queryable);
        var dtos = ObjectMapper.Map<List<BlogPost>, List<BlogPostDto>>(posts);

        return new PagedResultDto<BlogPostDto>(totalCount, dtos);
    }
}
