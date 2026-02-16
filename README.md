# Chaos

ABP-based modular application with Angular frontend and .NET 10 backend.

## Architecture

```
Chaos.slnx
├── src/
│   ├── Chaos.Domain.Shared/          # Shared constants, enums, localization
│   ├── Chaos.Domain/                 # Domain entities, repositories
│   ├── Chaos.Application.Contracts/  # DTOs, application service interfaces
│   ├── Chaos.Application/            # Application service implementations
│   ├── Chaos.EntityFrameworkCore/    # EF Core DbContext, migrations
│   ├── Chaos.HttpApi/               # API controllers
│   ├── Chaos.HttpApi.Client/        # HTTP client proxies
│   ├── Chaos.HttpApi.Host/          # API host (startup)
│   ├── Chaos.DbMigrator/            # Database migration console app
│   ├── Chaos.AppHost/               # .NET Aspire orchestration
│   ├── Chaos.ServiceDefaults/       # Shared service configuration
│   └── features/
│       └── Chaos.Features.Todo/     # Modular Todo feature (entity, app service, controller)
├── angular/                          # Angular 20 frontend (ABP 10.0.2)
└── test/                             # Unit and integration tests
```

## Feature Modules

### Todo
Self-contained feature module at `src/features/Chaos.Features.Todo/` containing:
- Entity, repository interface, EF Core configuration
- Application service, DTOs, contracts
- HTTP API controller
- Module class that wires everything together

## Seed Data

| User    | Password  |
|---------|-----------|
| admin   | 1q2w3E*   |

## Running with Docker Compose

```bash
docker compose up --build
```

- **API**: http://localhost:44342 (Swagger at /swagger)
- **Angular**: http://localhost:4200
- **PostgreSQL**: localhost:5432

## Running with Aspire (Development)

```bash
# Start the backend (PostgreSQL + DbMigrator + API)
dotnet run --project src/Chaos.AppHost

# In a separate terminal, start the Angular frontend
cd angular
npm install
npm start
```

The Aspire dashboard shows all service endpoints and health status.

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | /api/app/todo | List todos |
| POST   | /api/app/todo | Create todo |
| GET    | /api/app/todo/{id} | Get todo |
| PUT    | /api/app/todo/{id} | Update todo |
| DELETE | /api/app/todo/{id} | Delete todo |
