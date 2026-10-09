<!--
Sync Impact Report
==================
Version change: 1.1.0 → 1.2.0
Bump rationale: MINOR, because the schema changes materially (a field is removed from an entity).
This mirrors "adding a schema entity", which the versioning policy lists as MINOR. No principle is
removed or redefined. The bump type was confirmed by the product owner on 2026-10-09.

Modified principles: none
Modified sections:
- Domain Model & Data Constraints: `UserGame.AddedAt` (DateTime) removed from the entity table and
  the ERD. Product-owner decision of 2026-10-09: not needed for the MVP (YAGNI). Nothing had been
  migrated yet, so there is no data to preserve.
Added sections: none
Removed sections: none

Dependent docs:
- .context/02-data-model.md: ✅ updated (AddedAt removed from UserGame)
- .specify/templates/*: ✅ no edit needed (they do not reference individual fields)

Deferred intents (outside the constitution):
- Geen_Gezever.Core/Entities/UserGame.cs still has the `AddedAt` property. The developer removes it
  (mentor mode: Claude does not edit code).

Resolver note: `.specify/scripts/powershell/resolve-template.ps1` could not run because PowerShell
(pwsh) is not installed. No override, preset or extension layers exist (.specify/templates/overrides,
.specify/presets and .specify/extensions are all missing), so the core constitution-template.md is
the only layer. The existing structure was kept unchanged.

Deferred items / open clarifications (the source docs do not define these yet):
- TODO(DURATION_MATCH_DEFINITION): define what counts as an "exact duration match" against a game's
  MinPlayTimeMinutes..MaxPlayTimeMinutes range, and which bound the ±15-minute stretch is measured
  from.
- TODO(SOFT_SCORE_WEIGHTS): define the relative weights of duration proximity and vibe matching.
- TODO(SESSION_PERSISTENCE): decide whether the active source selection per game session is
  persisted server-side (no Session entity exists in the ERD) or held in client state only.
- TODO(SHARED_CONTRACTS): 01-architecture says Client references Core "for shared models/DTOs",
  while DTOs are assigned to the Api layer. Decide where the shared DTO contracts live.
-->

# Geen Gezever Constitution

Geen Gezever is a mobile-first companion app that picks the right board game for the people in the
room in **under 60 seconds**. It works with the games physically present tonight, including copies
brought by visiting friends, and surfaces them through a quick multi-parameter Match Finder or a
Tinder-style swipe deck. The `.context/` documents (00–04) are the detailed reference. This
constitution is the binding contract derived from them.

## Core Principles

### I. Layered Architecture & Dependency Boundaries (NON-NEGOTIABLE)

The solution is a 3-tier layered architecture with a separate client. Dependencies flow inward
toward Core only.

| Layer | Project | Responsibilities | Allowed references |
| --- | --- | --- | --- |
| Core | `Geen_Gezever.Core` (assembly `Geen_Gezever`) | Entities, value objects, domain logic, service interfaces, repository interfaces | None |
| Infrastructure | `Geen_Gezever.Infrastructure` | EF Core `DbContext`, migrations, repository implementations, BGG XML API client | Core |
| API | `Geen_Gezever.Api` | Controllers/minimal APIs, routing, DTOs, request validation, DI setup, OpenAPI | Core, Infrastructure |
| Client | `Geen_Gezever.Client` | Blazor WASM PWA pages, components, state containers, HTTP calls to the API | Core (shared models/DTOs only) |

- Core MUST have zero dependencies on external frameworks, databases, ORMs, or API/UI packages.
  Core MUST NOT reference Infrastructure, Api or Client.
- EF Core types (`DbContext`, `DbSet`, migrations, EF attributes or fluent configuration) MUST
  live in Infrastructure only.
- All database access MUST go through repository interfaces or dedicated domain query handlers
  defined in Core and implemented in Infrastructure.
- The Client MUST NOT receive a `DbContext`, `IQueryable`, or a raw persistence entity. Data
  crosses the API boundary only as typed DTOs.
- The Client MUST talk to the backend only over HTTP. It MUST NOT reference Infrastructure or Api.

**Rationale**: strict boundaries keep the matching logic framework-free and unit-testable. They also
allow the database to switch from SQLite to PostgreSQL without touching the domain or the UI.

### II. ERD Fidelity & Data Integrity

- The entities, keys and relationships in **Domain Model & Data Constraints** are authoritative.
  Any schema change MUST first amend this constitution, then ship as an EF Core migration.
- `Game.BggId` MUST be unique. A BGG title exists once in the `Game` catalog.
- `UserGame` MUST keep its own Guid `Id`, so one user can hold the same game more than once, for
  example the host's copy and a guest's copy.
- `UserGame.GuestOwnerName` is the only source of ownership: `null` means host-owned, and a
  non-null value names the visiting friend who brought that copy.
- `GameVibe` MUST use the composite primary key `(GameId, VibeId)`.

**Rationale**: the recommendation engine is only as correct as the ownership and metadata it reads.

### III. Room-First Selection

- Recommendations MUST only include `UserGame` copies from sources the host has marked active for
  the current game session. Sources are the host's own shelf and each guest owner.
- A game that is not physically present tonight MUST never be recommended, however well it scores.

**Rationale**: suggesting a game nobody brought wastes the time the app exists to save.

### IV. Strict Player Count, Soft Ranking

- The player count is a hard filter. It is never relaxed: not in the fallback, not in swipe mode,
  not in any future mode.
- Duration and vibe only influence ranking and scoring. They MUST NOT exclude a game on their own,
  except through the bounded result sets defined in **Matching Engine Specification**.

**Rationale**: a game that does not support the group size cannot be played, while a game that
runs slightly long or short often still can.

### V. Decisive, Not Endless

- The Match Finder MUST present a prominent Top 3 and MUST NOT present an unbounded list.
- The flow from opening the app to a chosen game MUST be achievable in under 60 seconds.
- An empty result MUST trigger the Smart Time Stretch fallback before the UI shows "no match".

**Rationale**: choice paralysis is the problem being solved, so more options is not a feature.

### VI. Frictionless Onboarding via BoardGameGeek

- Adding a game MUST work through BGG search and a single tap to link it. Users MUST NOT have to
  enter metadata by hand (players, play time, images).
- BGG XML API 2 calls MUST go through `IHttpClientFactory` with Polly resilience (exponential
  backoff and retry) and MUST respect BGG rate limits.
- Metadata MUST be cached in the local `Game` catalog. A BggId already present MUST NOT be fetched
  again for every user who adds it.

**Rationale**: shelf management is admin overhead, so it has to be close to invisible.

### VII. Mobile-First, Accessible & Table-Respectful UI

- Every screen MUST be designed for one-handed phone use first, in dim, cozy lighting.
- Interactive targets MUST be at least 48×48 px (WCAG 2.5.5).
- The UI MUST meet WCAG 2.1 AA: text contrast ≥ 4.5:1, and graphical or card elements ≥ 3:1.
- Swipe gestures MUST animate smoothly at 60 fps, using lightweight JS interop
  (`touchstart`/`touchmove`/`touchend`) or CSS touch actions.
- The app MUST NOT add engagement mechanics (notifications, feeds, timers) that keep the phone on
  the table once a game has been chosen.

**Rationale**: the app is a quick decision tool. Once the game is picked, the phone goes away.

### VIII. Senior Mindset — No Assumptions, No Unilateral Decisions

This applies to everyone who contributes: AI assistants and human developers alike.

- Contributors MUST approach every task as a senior developer would: challenge unclear
  requirements, flag risks, trade-offs and inconsistencies, and think through edge cases before
  writing code.
- Contributors MUST NOT make assumptions. Anything that does not follow directly from this
  constitution or the `.context/` documents MUST be turned into an explicit question to the
  product owner.
- Contributors MUST NOT make a decision on their own when that decision is not already recorded in
  this constitution or `.context/`. This covers, among other things, naming, libraries and
  dependencies, schema changes, UX flows, scoring rules and API contracts. Contributors MUST present
  the options, each with its trade-offs and a recommendation, and wait for approval.
- Trivial, purely local implementation details that have no effect on behaviour, contracts or
  structure are exempt. Examples are formatting and the names of local variables.
- Once a question has been answered, the answer MUST be recorded in the relevant artifact (spec,
  plan, `.context/` or this constitution). Otherwise the same question comes back later.

**Rationale**: silent assumptions cause rework and misaligned features. Asking first keeps the
product owner and the implementer on the same page.

## Domain Model & Data Constraints

### Entities

| Entity | Field | Type | Notes |
| --- | --- | --- | --- |
| **User** | `Id` | Guid | PK |
| | `Username` | string | |
| | `Email` | string | |
| **Game** | `Id` | Guid | PK |
| | `BggId` | int | Unique index |
| | `Title` | string | |
| | `ThumbnailUrl` | string | |
| | `ImageUrl` | string | |
| | `MinPlayers` | int | Used by the hard filter |
| | `MaxPlayers` | int | Used by the hard filter |
| | `MinPlayTimeMinutes` | int | Used by the duration soft filter |
| | `MaxPlayTimeMinutes` | int | Used by the duration soft filter |
| | `RulesUrl` | string | |
| **UserGame** | `Id` | Guid | PK. Allows duplicate copies of a game per user |
| | `UserId` | Guid | FK → User |
| | `GameId` | Guid | FK → Game |
| | `GuestOwnerName` | string? | `null` = host-owned, otherwise the guest's name |
| | `GameLocation` | string? | Free-text shelf/spot. Replaced by `ClosetId` in Sprint 2 |
| **Vibe** | `Id` | int | PK |
| | `Name` | string | |
| | `Slug` | string | URL-safe identifier |
| **GameVibe** | `GameId` | Guid | Composite PK part, FK → Game |
| | `VibeId` | int | Composite PK part, FK → Vibe |

### Entity Relationship Diagram

```mermaid
erDiagram
    USER ||--o{ USER_GAME : "owns / hosts"
    GAME ||--o{ USER_GAME : "is copied as"
    GAME ||--o{ GAME_VIBE : "has"
    VIBE ||--o{ GAME_VIBE : "tags"

    USER {
        Guid Id PK
        string Username
        string Email
    }
    GAME {
        Guid Id PK
        int BggId UK
        string Title
        string ThumbnailUrl
        string ImageUrl
        int MinPlayers
        int MaxPlayers
        int MinPlayTimeMinutes
        int MaxPlayTimeMinutes
        string RulesUrl
    }
    USER_GAME {
        Guid Id PK
        Guid UserId FK
        Guid GameId FK
        string GuestOwnerName "nullable; null = host"
        string GameLocation "nullable"
    }
    VIBE {
        int Id PK
        string Name
        string Slug
    }
    GAME_VIBE {
        Guid GameId PK, FK
        int VibeId PK, FK
    }
```

## Matching Engine Specification

The matching rules are domain logic and MUST be implemented in Core. They MUST be covered by unit
tests for each rule below.

1. **Candidate set**: the `UserGame` copies from the active sources of the current session
   (Principle III).
2. **Hard filter (player count)**: keep a game only when
   `MinPlayers <= RequestedPlayers AND MaxPlayers >= RequestedPlayers`. A game that does not
   support the exact requested player count is excluded, with no exceptions.
3. **Soft filters (ranking)**: the remaining games are scored and ordered by:
   - **Duration proximity**: how close the game's play time is to the requested duration.
   - **Vibe match**: overlap between the requested vibes and the game's `GameVibe` tags.
4. **Output**: the Top 3 highest-scoring games (Principle V).
5. **Empty match fallback (Smart Time Stretch)**: when zero games match the requested duration
   exactly, return instead:
   - at most **2** games whose duration is **at most 15 minutes shorter** than requested, and
   - at most **1** game whose duration is **at most 15 minutes longer** than requested.

   The player-count hard filter stays strictly enforced during the fallback.
6. **Tinder swipe mode**: the deck combines exact duration matches with games inside the
   **±15-minute buffer**, so there is enough variety to swipe through. The hard filter and the
   active-source rule still apply to every card.

## Technology Stack & Quality Standards

- **Runtime**: .NET 10 (`net10.0`) and C# for all projects.
- **API**: RESTful ASP.NET Core Web API with typed DTO contracts, request validation and
  OpenAPI documentation.
- **Persistence**: Entity Framework Core. SQLite for local development, PostgreSQL as the
  production target. Code MUST NOT rely on provider-specific behaviour that breaks either one.
- **External integration**: BGG XML API 2 via `IHttpClientFactory` + Polly (Principle VI).
- **Frontend**: Blazor WebAssembly as an installable PWA, with a service worker for asset caching
  and standalone launch from the iOS/Android home screen.
- **Client state**: scoped C# state container services injected into components.
- **Styling**: Tailwind CSS, utility-first.
- **Tooling**: Visual Studio Code with the C# Dev Kit, and Git with a feature-branch workflow.

## Development Workflow & Quality Gates

- Every feature follows the Spec Kit flow: `/speckit-specify` → `/speckit-clarify` (when needed)
  → `/speckit-plan` → `/speckit-tasks` → `/speckit-implement`.
- Every `plan.md` MUST pass a Constitution Check against Principles I–VIII before design starts,
  and again after design. Any justified deviation MUST be recorded in the plan's Complexity
  Tracking table.
- Work happens on feature branches. Merging requires the solution to build and all tests to pass.
- Matching engine changes MUST come with unit tests in Core. Schema changes MUST come with an EF
  Core migration that applies cleanly to SQLite.
- AI assistants MUST read `.context/` and this constitution before generating code, and MUST
  respect the layer boundaries and ERD without exception.

## Roadmap: Sprint 2 — Multi-Closet System

This is a planned direction and not yet binding schema. It becomes binding through a MINOR
amendment of this constitution when Sprint 2 is specified.

- Introduce a **Closet** entity: `Id` (Guid), `UserId` (Guid, FK → User), `Name` (string, for
  example "Living Room Kallax" or "Basement"), `Description` (string?).
- Relate **User 1-to-many Closet**.
- Migrate `UserGame.GameLocation` from a free-text string to a foreign key `UserGame.ClosetId`
  (FK → Closet), plus an optional free-text slot/shelf detail string.
- The migration MUST preserve existing `GameLocation` data, for example by seeding closets from
  distinct values or by moving the text into the slot/shelf detail field.

```mermaid
erDiagram
    USER ||--o{ CLOSET : "has"
    CLOSET ||--o{ USER_GAME : "stores"
```

## Governance

- This constitution overrides all other project practices. The `.context/` documents are
  explanatory references. When they conflict with this constitution, this constitution wins
  until it is amended.
- **Amendments** MUST be made through `/speckit-constitution`. Each amendment MUST include a Sync
  Impact Report, update the version and Last Amended date, and update `.context/` when those
  documents are affected.
- **Versioning** follows semantic versioning:
  - MAJOR: removing or redefining a principle, or making a backward-incompatible governance change.
  - MINOR: adding a principle or section, materially expanding guidance, or adding a schema entity.
  - PATCH: clarifications, wording and typo fixes.
- **Compliance**: every plan and code review MUST check layer boundaries, ERD fidelity and the
  matching rules. Additional complexity MUST be justified in writing.
- `.context/CLAUDE.md` holds the runtime guidance for AI assistants.

**Version**: 1.2.0 | **Ratified**: 2026-09-29 | **Last Amended**: 2026-10-09
