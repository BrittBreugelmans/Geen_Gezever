# Architectural Rules & Layer Constraints

## Layers & Responsibilities
1. **Core (`Geen_Gezever`):**
   - Pure domain logic, entities, value objects, service interfaces, repository interfaces.
   - ZERO dependencies on external frameworks, databases, or API/UI packages.
   - NEVER reference `Infrastructure`, `Api`, or `Client`.

2. **Contracts (`Geen_Gezever.Contracts`):**
   - DTOs (request and response shapes) shared by API and Client. Plain data, no logic.
   - References nothing.

3. **Infrastructure (`Geen_Gezever.Infrastructure`):**
   - EF Core `DbContext`, migrations, repository implementations, external APIs (BGG).
   - References `Core`.

4. **API (`Geen_Gezever.Api`):**
   - ASP.NET Core controllers, minimal APIs, endpoint routing, mapping between entities and DTOs, request validation, DI container setup.
   - References `Core`, `Infrastructure` and `Contracts`.

5. **Client (`Geen_Gezever.Client`):**
   - Blazor WebAssembly (PWA), UI components, pages, state management, HTTP client calls to API.
   - References `Contracts` only.

## Inviolable Rules
- Never leak `DbContext` or raw database entities directly to the `Client`.
- All database queries go through repositories or specific domain query handlers.
- When generating code, always respect this layer separation.