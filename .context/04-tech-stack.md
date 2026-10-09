# 04 - Technical Stack & Configuration

## 1. Solution Structure (.NET Solution)
- `Geen_Gezever.Core`: Domain entities, value objects, domain logic, service contracts, repository interfaces. Zero third-party dependencies.
- `Geen_Gezever.Contracts`: DTOs shared by Api and Client. No references to other projects.
- `Geen_Gezever.Infrastructure`: EF Core `DbContext`, SQL migrations, BGG XML API client, repository implementations.
- `Geen_Gezever.Api`: ASP.NET Core Web API, controllers/endpoints, validation, OpenAPI/Swagger configuration.
- `Geen_Gezever.Client`: Blazor WebAssembly PWA, UI components, client-side state, HTTP client calls.

## 2. Backend & Runtime
- **Framework:** .NET 9 / 10 (C#)
- **API Architecture:** RESTful Web API with typed DTO contracts.
- **ORM:** Entity Framework Core.
- **Database:** SQLite (local development) / PostgreSQL (production target).
- **HTTP Client & Resilience:** `IHttpClientFactory` configured with Polly for rate-limited BGG XML API 2 requests (exponential backoff & retry).

## 3. Frontend & PWA
- **Framework:** Blazor WebAssembly (.NET WASM).
- **Application Type:** Progressive Web App (PWA) with service worker for fast asset caching and standalone home-screen launch on iOS/Android.
- **State Management:** Scoped C# state container services injected into Blazor components.
- **Session State:** The active source selection for a game session (which closets and guests are in play tonight) lives client-side only, persisted to `localStorage`. No server-side session.
- **Styling:** Tailwind CSS with utility-first layout classes.

## 4. UI/UX Standards & Accessibility
- **Touch Targets:** Minimum $48 \times 48\,\text{px}$ on all interactive triggers (WCAG 2.5.5).
- **Accessibility:** WCAG 2.1 AA compliance (contrast ratios $\ge 4.5:1$ for standard text, $\ge 3:1$ for graphical/card elements).
- **Gestures:** Touch event binding (`touchstart`, `touchmove`, `touchend`) via lightweight JavaScript interop or CSS touch actions for smooth 60fps swipe animations.

## 5. Development & Tooling
- **Editor:** Visual Studio Code with C# Dev Kit extension.
- **Version Control:** Git with feature-branch workflow.
- **LLM Context:** Standardized `.context/` docs and `constitution.md` root contract.