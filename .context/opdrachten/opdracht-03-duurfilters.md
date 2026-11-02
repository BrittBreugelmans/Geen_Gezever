# Opdracht 3: De duurfilters, met unit tests

## Doel

Bouw de drie duurregels uit de Match Finder als methodes op `Game`, telkens volgens **Red → Green → Refactor**.

Dit is hetzelfde soort werk als opdracht 2, maar met minder begeleiding. Probeer zo ver mogelijk te geraken zonder hulp, en noteer waar je vastliep.

## De regels

Uit de constitution (Matching Engine Specification, regels 3 en 6) en [03-business-rules.md](../03-business-rules.md). `d` is de gevraagde duur in minuten.

| Regel | Voorwaarde | Voorbeeld bij `d = 60` |
|---|---|---|
| **Exacte match** | `MinPlayTimeMinutes <= d AND MaxPlayTimeMinutes >= d` | 45–90 ✅ |
| **Korter, binnen de stretch** | `d - 15 <= MaxPlayTimeMinutes < d` | 50–55 ✅ |
| **Langer, binnen de stretch** | `d < MinPlayTimeMinutes <= d + 15` | 70–80 ✅ |

Lees de tekens heel nauwkeurig: `<` en `<=` wisselen elkaar af. Dat is geen typfout.

## Stappen

- [X] Merge opdracht 2 en maak een nieuwe feature branch vanaf `dev`
- [X] Schrijf per regel eerst je testgevallen **op papier**: welke waarden, en wat verwacht je?
- [X] **Exacte match:** Red → Green
- [X] **Korter binnen de stretch:** Red → Green
- [X] **Langer binnen de stretch:** Red → Green
- [X] **Refactor:** zowel de code in `Game` als je tests
- [X] `dotnet test` toont alle tests groen

## Nieuw in deze opdracht

In opdracht 2 had je twee Theories: één voor `true` en één voor `false`. `[InlineData]` kan ook **meerdere waarden** meegeven, bijvoorbeeld de invoer én het verwachte resultaat. Zoek op hoe dat werkt en beslis zelf of je het gebruikt. Wat wordt er leesbaarder, en wat minder?

## Om over na te denken (maximaal twee punten)

1. **Het getal 15** komt in twee regels voor. Waar zet je dat, en hoe noem je het? Wat gebeurt er als Kevin later zegt: "maak er 20 van"?
2. **Kan één game aan twee regels tegelijk voldoen?** Bijvoorbeeld exact én korter? Probeer het eerst te beredeneren met de voorwaarden uit de tabel, en schrijf daarna een test die je antwoord bewijst.

## Tips voor je testgevallen

- Voor elke `<=` en elke `<` bestaat er een grenswaarde: een waarde die er *net op* ligt en een die er *net naast* ligt. Hoeveel grenzen heeft de regel "korter"?
- Gebruik de voorbeelden uit de tabel als eerste "happy path".
- Baseer je verwachting **altijd op de regel**, nooit op wat je code toevallig doet (zie opdracht 2 😉).

## Klaar wanneer

- Alle tests zijn groen.
- `dotnet build` geeft geen warnings.
- Je kunt voor elke regel uitleggen waarom er `<` of `<=` staat, met een voorbeeld van een game die net wel of net niet meedoet.

## Buiten scope (komt later)

- **Opdracht 4:** vibe-overlap. Daarvoor moet je eerst beslissen hoe een `Game` weet welke vibes hij heeft.
- **Opdracht 5:** een `MatchFinder` die alles combineert: ranking, top 3 en de Smart Time Stretch-limieten (max 2 korter, max 1 langer).

## Documentatie

- [Unit testing C# with xUnit](https://learn.microsoft.com/dotnet/core/testing/unit-testing-csharp-with-xunit)
- [Constants (C#)](https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/constants)
- [Comparison operators](https://learn.microsoft.com/dotnet/csharp/language-reference/operators/comparison-operators)
