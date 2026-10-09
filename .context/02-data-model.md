# Data Model & Schema Rules

## Entities
- **User:** `Id` (Guid), `Username`, `Email`.
- **Game:** `Id` (Guid), `BggId` (int, Unique index), `Title`, `ThumbnailUrl` (string?), `ImageUrl` (string?), `MinPlayers`, `MaxPlayers`, `MinPlayTimeMinutes`, `MaxPlayTimeMinutes`, `RulesUrl` (string?, not supplied by BGG).
- **UserGame:** `Id` (Guid), `UserId` (Guid), `GameId` (Guid), `GuestOwnerName` (string?, null = host owned), `GameLocation` (string?, optional description of shelf/spot).
- **Vibe:** `Id` (int), `Name` (string), `Slug` (string, URL-safe identifier).
- **GameVibe:** `GameId` (Guid), `VibeId` (int). Composite primary key `(GameId, VibeId)`.

## Important Logic
- `UserGames` has its own Guid `Id` so duplicate games can exist under one user if a guest brings their own copy.
- `GuestOwnerName` determines whether a game belongs to the host or a visiting friend.