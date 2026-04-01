# Blog Feature Module — Design Decisions

## Architecture

The Blog feature follows the established modular monolith pattern used by Todo and Shopping features:

- **Single project**: `Chaos.Features.Blog` contains Domain, Application, Infrastructure, and UI layers
- **Namespace**: `Chaos` (flat, matching convention)
- **SDK**: `Microsoft.NET.Sdk.Razor` for Blazor page support

## Key Decisions

### Markdown Rendering
- **Markdig** for server-side Markdown→HTML conversion (CommonMark compliant)
- Code blocks styled via scoped CSS on the blog post view page
- No client-side JS syntax highlighter needed — CSS-based styling with monospace fonts provides readable code blocks while keeping dependencies minimal

### Public vs Admin Pages
- `/blog` and `/blog/{slug}` — `[AllowAnonymous]` for public access
- `/blog/manage` — `[Authorize]` for admin CRUD
- The `/` route also maps to the blog landing page

### Content Storage
- Blog content stored as raw Markdown in the `Content` column (no max length constraint)
- Rendered to HTML at read time using Markdig
- Slug field with unique index for SEO-friendly URLs

### Seed Data
- 3 sample posts with real Markdown content including fenced code blocks
- Demonstrates C#, Bash, Dockerfile, YAML code blocks
- All seeded as `Published` status

### Permissions
- Public reading is anonymous (via `GetPublishedListAsync` with `[AllowAnonymous]`)
- CRUD operations require `Chaos.Blog` permissions
- Follows same pattern as Todo/Shopping permission groups

## Changelog

### v1.0.0 (2026-04-01)
- Initial implementation of Blog feature module
- Public landing page with card layout at `/blog`
- Single post view with Markdown rendering at `/blog/{slug}`
- Admin CRUD at `/blog/manage` with FluentDataGrid and FluentDialog
- BlogPost entity with Draft/Published/Archived status workflow
- Seed data with 3 sample posts containing code blocks
- en + tr localization
- Markdig integration for Markdown processing
