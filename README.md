# ResQLine — Backend Documentation

Status: concise, developer-focused reference covering architecture, how to run, core components, DB schema notes, common commands, and extension points.

---

## 1. Overview

ResQLine is a .NET 9 minimal API backend that provides emergency reporting, OTP-based authentication, SOS location, and admin/responder tooling. It follows Clean Architecture with separate Application, Domain, Infrastructure, SharedKernel, and Web.Api (presentation) layers.

Core responsibilities:
- Authentication (OTP + JWT)
- Report ingestion (text, images, geolocation)
- SOS/location trigger
- Domain events publishing
- Persistence with EF Core + PostgreSQL

---

## 2. Repository layout (paths you will use)

- `src/Application/` — Use cases, DTOs, Commands, Handlers
- `src/Domain/` — Entities, Value Objects, Domain Events
- `src/Infrastructure/` — EF Core, external integrations, DI
- `src/Infrastructure/Database/` — `ApplicationDbContext`, EF configurations, `Migrations/`
- `src/SharedKernel/` — base `Entity`, domain event interfaces, helpers
- `src/Web.Api/` — Minimal API endpoints, `Program.cs`, startup wiring
- `tests/` — architecture & unit/integration tests

Important files referenced in this repo:
- `src/Web.Api/Program.cs` — app startup, services registration, migrations apply in Development
- `src/Infrastructure/Database/ApplicationDbContext.cs` — EF DbContext and domain event dispatching
- `src/Infrastructure/Database/Migrations/` — EF migration files (example: `20251213035625_added_ReverseGeoCode_On_location.cs`)
- `README.md`, `CONTRIBUTING.md`, `.editorconfig` — project docs & coding standards

---

## 3. Running & developing locally

Prerequisites:
- .NET 9 SDK
- PostgreSQL (or Docker)
- Git
- Optional: Docker Desktop

Common commands (from repo root):

- Restore:
  dotnet restore

- Build:
  dotnet build

- Run (development):
  dotnet run --project src/Web.Api

- Run tests:
  dotnet test

- Format (enforce project styles):
  dotnet format

Visual Studio helpers:
- Use __Format Document__ before committing.
- Recommended IDE path for settings: __Tools > Options > Text Editor > C#__ to ensure editor respects `.editorconfig`.

Notes:
- App loads environment variables using DotNetEnv; local `.env` can be used for development secrets.
- `Program.cs` calls `ApplyMigrations()` in Development. Migrations live at `src/Infrastructure/Database/Migrations`.

---

## 4. Database & EF Core

DbContext:
- `ApplicationDbContext` exposes `DbSet<User> Users`, `DbSet<Report> Reports`, and `DbSet<OtpStore> OtpStores`.
- `OnModelCreating` applies all EF configurations from assembly and sets default schema from `SharedKernel.Schemas.Default`.
- `SaveChangesAsync` commits and then publishes domain events via `IDomainEventsDispatcher`.

Migrations:
- Location: `src/Infrastructure/Database/Migrations/`
- Example migration file: `20251213035625_added_ReverseGeoCode_On_location.cs` adds a non-null `ReportedAt_ReverseGeoCode` text column to `public.Reports`.

Common EF commands:
- Add migration:
  dotnet ef migrations add <Name> --project src/Infrastructure --startup-project src/Web.Api
- Apply migration to DB:
  dotnet ef database update --project src/Infrastructure --startup-project src/Web.Api

The application also auto-applies migrations during startup in Development mode.

---

## 5. Authentication & Endpoints

Authentication:
- OTP flows exist (`src/Web.Api/Endpoints/Otps/Register/Send.cs`, `.../Login/Send.cs`) — used to verify phone numbers and issue JWT.
- JWT-based authentication is configured in the Infrastructure layer. `Program.cs` registers authentication and maps endpoints.

Endpoints:
- Minimal API endpoints are discovered / mapped via `builder.Services.AddEndpoints(Assembly.GetExecutingAssembly())` and `app.MapEndpoints()`.
- Controllers are supported but minimal API is the default; `app.MapControllers()` is present if controllers are added.

Health checks:
- `app.MapHealthChecks("health", ...)` with UI response writer is configured in `Program.cs`.

---

## 6. Logging, Telemetry, & Error Handling

- Serilog is used for structured logging and request logging:
  - Configured via `builder.Host.UseSerilog(...)` and `app.UseSerilogRequestLogging()`.
- Error handling:
  - `app.UseExceptionHandler()` is registered for centralized error handling.
- Request context logging middleware exists: `app.UseRequestContextLogging()`.

---

## 7. Domain Events & Patterns

- Entities inherit from a shared `Entity` type that collects `IDomainEvent` instances.
- `ApplicationDbContext.SaveChangesAsync` publishes domain events after EF commit by dispatching events through `IDomainEventsDispatcher`.
- This provides eventual-consistency semantics by design (publish-after-commit).

Guideline: Keep domain invariants in Domain layer; coordinate side-effects (notifications, integrations) via domain events and handlers in Infrastructure/Application.

---

## 8. Tests & CI

- Unit and architecture tests are in `tests/`.
- CI should run:
  1. dotnet restore
  2. dotnet build
  3. dotnet test
  4. dotnet format --verify-no-changes

Ensure migrations are validated in CI for schema changes.

---

## 9. Deployment & Docker

- Docker and Docker Compose files exist at repository root for orchestration.
- README mentions Render as a hosting target; CI/CD should produce container images that include necessary env vars for DB, JWT secrets, and provider credentials (SMS provider, etc.).

---

## 10. Common extension tasks

Add a new endpoint:
1. Create request/response DTOs in `src/Application/` if it contains business logic.
2. Add an endpoint class under `src/Web.Api/Endpoints/<area>/`.
3. Register any services in `src/Infrastructure/DependencyInjection.cs` or appropriate DI module.
4. Add EF configuration and migration if the model changed.

Add a new entity:
1. Add domain model in `src/Domain/`.
2. Add EF configuration in `src/Infrastructure/Database/Configurations/`.
3. Add migration:
   dotnet ef migrations add AddMyEntity --project src/Infrastructure --startup-project src/Web.Api

---

## 11. Useful files to inspect when debugging

- `src/Web.Api/Program.cs` — entry & startup behavior
- `src/Infrastructure/Database/ApplicationDbContext.cs` — persistence & domain-event publishing
- `src/Infrastructure/DependencyInjection.cs` — how infrastructure services are wired
- `src/Infrastructure/Database/Migrations/` — schema changes
- `src/Web.Api/Endpoints/` — current HTTP routes and behavior
- `README.md`, `CONTRIBUTING.md`, `.editorconfig` — guidelines and coding rules

---

## 12. Quick reference commands

- dotnet restore
- dotnet build
- dotnet run --project src/Web.Api
- dotnet test
- dotnet format
- dotnet ef migrations add <Name> --project src/Infrastructure --startup-project src/Web.Api
- dotnet ef database update --project src/Infrastructure --startup-project src/Web.Api

---

## 13. Where to add documentation contributions

- Add high-level docs under `docs/` (this file lives there).
- Add per-feature README.md files in the layer folder when the feature grows in complexity.
- Update `CONTRIBUTING.md` for workflow changes (coding rules are enforced by `.editorconfig`).

---

If you want, I can:
- Expand this document into a multi-page docs site (Endpoints reference, DB schema, auth flow diagrams).
- Generate OpenAPI (Swagger) summary of endpoints.
- Produce entity-relationship summary from EF model snapshot.

Which one do you want next?