using System;
using System.Threading.Tasks;
using Chaos.Domain;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Chaos.Infrastructure;

public class BlogDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<BlogPost, Guid> _blogPostRepository;

    public BlogDataSeedContributor(IRepository<BlogPost, Guid> blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _blogPostRepository.GetCountAsync() > 0)
        {
            return;
        }

        await _blogPostRepository.InsertManyAsync(new[]
        {
            new BlogPost
            {
                Title = "Getting Started with ASP.NET Core",
                Slug = "getting-started-with-aspnet-core",
                Summary = "A beginner-friendly guide to building web applications with ASP.NET Core and C#.",
                Content = @"# Getting Started with ASP.NET Core

ASP.NET Core is a cross-platform, high-performance framework for building modern web applications.

## Creating Your First Project

Use the .NET CLI to scaffold a new project:

```bash
dotnet new webapp -n MyFirstApp
cd MyFirstApp
dotnet run
```

## Adding a Controller

Controllers handle incoming HTTP requests. Here's a simple example:

```csharp
[ApiController]
[Route(""api/[controller]"")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = ""Hello, World!"" });
    }
}
```

## Dependency Injection

ASP.NET Core has built-in dependency injection:

```csharp
builder.Services.AddScoped<IMyService, MyService>();
```

This makes your code testable and loosely coupled.

## Next Steps

- Explore middleware pipeline
- Add Entity Framework Core for data access
- Implement authentication with Identity
",
                Status = BlogStatus.Published,
                PublishedAt = new DateTime(2026, 3, 15, 10, 0, 0, DateTimeKind.Utc)
            },
            new BlogPost
            {
                Title = "Understanding Blazor Components",
                Slug = "understanding-blazor-components",
                Summary = "Learn how to build interactive UI components with Blazor and FluentUI.",
                Content = @"# Understanding Blazor Components

Blazor lets you build interactive web UIs using C# instead of JavaScript.

## Component Basics

A Blazor component is a `.razor` file with markup and C# code:

```csharp
@page ""/counter""

<h1>Counter</h1>
<p>Current count: @currentCount</p>
<button @onclick=""IncrementCount"">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

## Parameters

Pass data to components using parameters:

```csharp
[Parameter]
public string Title { get; set; } = ""Default"";

[Parameter]
public EventCallback<string> OnTitleChanged { get; set; }
```

## Using FluentUI Components

FluentUI provides a rich set of Blazor components:

```html
<FluentTextField @bind-Value=""name"" Label=""Your Name"" />
<FluentButton Appearance=""Appearance.Accent"" OnClick=""Submit"">
    Save
</FluentButton>
```

These components follow Microsoft's Fluent Design System for a consistent look and feel.
",
                Status = BlogStatus.Published,
                PublishedAt = new DateTime(2026, 3, 20, 14, 30, 0, DateTimeKind.Utc)
            },
            new BlogPost
            {
                Title = "Docker for .NET Developers",
                Slug = "docker-for-dotnet-developers",
                Summary = "Containerize your .NET applications with Docker for consistent deployments.",
                Content = @"# Docker for .NET Developers

Docker simplifies deployment by packaging your app with all its dependencies.

## Basic Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY [""MyApp.csproj"", "".""]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [""dotnet"", ""MyApp.dll""]
```

## Docker Compose

For multi-container setups:

```yaml
services:
  web:
    build: .
    ports:
      - ""8080:8080""
    depends_on:
      - db
  db:
    image: postgres:17
    environment:
      POSTGRES_PASSWORD: example
```

## Tips

- Use multi-stage builds to keep images small
- Leverage `.dockerignore` to exclude unnecessary files
- Use health checks for production readiness
",
                Status = BlogStatus.Published,
                PublishedAt = new DateTime(2026, 3, 28, 9, 0, 0, DateTimeKind.Utc)
            }
        });
    }
}
