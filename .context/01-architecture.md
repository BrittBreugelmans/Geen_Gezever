# Architectural Rules & Layer Constraints

## Layers & Responsibilities
1. **Core (`Geen_Gezever`):**
   - Pure domain logic, entities, value objects, service interfaces, repository interfaces.
   - ZERO dependencies on external frameworks, databases, or API/UI packages.
   - NEVER reference `Infrastructure`, `Api`, or `Client`.

2. **Infrastructure (`Geen_Gezever.Infrastructure`):**
   - EF Core `DbContext`, migrations, repository implementations, external APIs (BGG).
   - References `Core`.

3. **API (`Geen_Gezever.Api`):**
   - ASP.NET Core controllers, minimal APIs, endpoint routing, DTOs, request validation, DI container setup.
   - References `Core` and `Infrastructure`.

4. **Client (`Geen_Gezever.Client`):**
   - Blazor WebAssembly (PWA), UI components, pages, state management, HTTP client calls to API.
   - References `Core` (only for shared models/DTOs).

## Inviolable Rules
- Never leak `DbContext` or raw database entities directly to the `Client`.
- All database queries go through repositories or specific domain query handlers.
- When generating code, always respect this layer separation.