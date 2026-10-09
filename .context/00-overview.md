
# 00 - Project Overview & Vision

## Product Name
**Geen Gezever** (Internal Working Title: *Geen Gezever*)

## Mission & Problem Statement
Tabletop game nights often suffer from "choice paralysis": groups spend 20 to 45 minutes debating what to play while digging through game shelves, causing energy and momentum to stall.

**Geen Gezever** is a mobile-first companion app designed to pick the ideal board game in **under 60 seconds**. It aggregates games physically present in the room (including games brought along by visiting friends) and surfaces recommendations through quick multi-parameter matching or a tactile Tinder-style swipe interface.

## Target Audience & Environment
- **Setting:** Living rooms and gaming tables with cozy or dimmed lighting, drinks, and snacks.
- **Users:** Casual players to enthusiastic board gamers who prioritize playing over administrative overhead.
- **Usage Pattern:** One-handed mobile usage passed around or controlled by the host.

## Core Philosophy
1. **Frictionless Onboarding:** Adding games via BoardGameGeek (BGG) search takes one tap without manual metadata input.
2. **Room-First Selection:** Only recommend games that are physically present tonight.
3. **Decisive, Not Endless:** Show a prominent Top 3 to force a decision instead of infinite lists.
4. **Offline Table Respect:** The app is a lightweight decision tool, not a continuous screen distraction. Once the game is chosen, the phone goes away.

## MVP Scope (Sprint 1)
- Shelf management (Search & link via BGG).
- Guest game ownership tagging (`GuestOwnerName`).
- Active source selection per game session.
- Match Finder with hard player filters and soft duration/vibe scoring.
- Empty match fallback (smart 15-minute time stretch).
- Interactive card swipe mode.

## Future Architecture Roadmap (Sprint 2 Preview)
- **Multi-Closet System:**
  - Introduce `Closet` entity: `Id` (Guid), `UserId` (Guid), `Name` (string, e.g. "Living Room Kallax", "Basement"), `Description` (string?).
  - Relate `User` 1-to-many with `Closet`.
  - Migrate `UserGame.GameLocation` from an open string to a foreign key relation (`ClosetId`) with an optional slot/shelf detail string.