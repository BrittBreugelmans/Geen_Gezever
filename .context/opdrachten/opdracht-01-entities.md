# Opdracht 1: Domain entities in Core

## Doel

Zet de vijf entities uit [02-data-model.md](../02-data-model.md) om naar C#-klassen in `Geen_Gezever.Core`.

## Stappen

- [X] Verwijder `Class1.cs`
- [X] Maak een map voor de entities, met één bestand per klasse
- [X] `Vibe`: begin hiermee, dit is de eenvoudigste
- [X] `User`
- [X] `Game`
- [X] `UserGame`
- [ ] `GameVibe`
- [ ] `dotnet build` in de terminal

## Klaar wanneer

- Het build zonder errors en zonder warnings.
- Je kunt elke keuze die je maakte in eigen woorden uitleggen.

## Tijdens het bouwen

Noteer welke warnings je krijgt, ook als je nog niet weet wat ze betekenen. Die bespreken we in de review.

## Al besproken

### De lagen

| Laag | Inhoud |
|---|---|
| **Core** | Domeinmodellen, business logic (bv. de Smart Time Stretch) en interfaces zoals `IGameRepository` |
| **Infrastructure** | EF Core, database, BGG-API. Implementeert de interfaces uit Core |
| **Api** | HTTP-ingang: endpoints, DTO's, invoervalidatie, DI. Bevat geen eigen logica |
| **Client** | Blazor UI |

- Alles wijst naar Core, Core wijst naar niets (*dependency inversion*).
- Invoervalidatie ("is het aantal spelers ingevuld?") hoort in de Api. Domeinregels ("`MinPlayers` mag niet groter zijn dan `MaxPlayers`") horen in Core.

### `class` of `record`?

- Een **entity** blijft hetzelfde ding, ook als de waarden veranderen. De identiteit zit in het `Id`. → `class`
- Een **value object** *is* zijn waarden. → `record`
- Test: hernoem de vibe "gezellig" naar "knus". Het blijft dezelfde vibe, met hetzelfde `Id` en dezelfde gekoppelde games. Dus is `Vibe` een `class`.
- Records gebruik je later voor DTO's en value objects, bijvoorbeeld een speeltijd van "45–60 min".

**Besluit:** alle vijf entities worden een `class`. Over `GameVibe` hebben we het nog in de review.

## Open vragen

- **BGG zonder spelersaantal:** wat doen we als BGG geen spelersaantal teruggeeft, of `0`? Staat ook in `Game.cs` en in [vragen-voor-kevin.md](vragen-voor-kevin.md).

## Voor de review (pas bekijken als het build)

1. **Warning CS8618**: wat probeert de compiler je te zeggen, en welke oplossingen zijn er? Zoek het `required`-keyword op.
2. **Nullable properties**: `GuestOwnerName` en `GameLocation` zijn volgens het datamodel nullable. Wat met `ThumbnailUrl` en `RulesUrl`? Beslis dat bewust of noteer het als open vraag (Principe VIII).
3. **`[Key]` en `[Required]`**: horen die attributen in Core? Zie Principe I in de constitution.
4. **Navigation properties** (bv. `Game.UserGames`): heb je die nu al nodig?

## Documentatie

- [Nullable reference types](https://learn.microsoft.com/dotnet/csharp/nullable-references)
- [`required` modifier](https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/required)
- [Records](https://learn.microsoft.com/dotnet/csharp/fundamentals/types/records)
- [Common web application architectures](https://learn.microsoft.com/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
