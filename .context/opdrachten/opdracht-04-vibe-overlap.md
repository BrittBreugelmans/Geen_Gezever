# Opdracht 4: Vibe-overlap, eerst code en dan tests

## Doel

Bouw de vibe-match uit de Match Finder: een methode die telt **hoeveel van de gevraagde vibes** een game heeft.

In opdracht 2 en 3 werkte je test-driven. Deze keer draai je het om: eerst schrijf je de code, daarna de tests. Zo werken veel teams in de praktijk. Je oefent hier ook hoe je vermijdt dat je tests gewoon nazeggen wat je code doet.

Probeer zo ver mogelijk te geraken zonder hulp, en noteer waar je vastliep.

## De regel

Uit de constitution (Matching Engine Specification, regel 4.1):

> **Vibe match** (primary): the number of requested vibes that the game has as `GameVibe` tags. More overlap ranks higher.

En uit Principe IV: vibe beïnvloedt alleen de ranking. Een game met **0** overlap wordt dus niet uitgesloten, die scoort gewoon laag.

| Gevraagde vibes | Vibes van de game | Overlap |
|---|---|---|
| gezellig, strategisch | gezellig, chaotisch | 1 |
| gezellig | strategisch | 0 |

## Vooraf beslissen

Dit zijn jouw keuzes. Noteer wat je kiest en waarom.

1. **Hoe weet een `Game` welke vibes hij heeft?** Op dit moment weet `Game` daar niets van. `GameVibe` bestaat wel, maar er is geen verband. Een paar mogelijkheden:
   - een collectie op `Game` die de gekoppelde vibes bijhoudt;
   - de vibes van de game als parameter meegeven aan de methode;
   - iets anders dat je zelf bedenkt.

   Stel jezelf bij elke optie twee vragen. Blijft Core vrij van EF Core (zie [01-architecture.md](../01-architecture.md))? En wie moet de vibes "invullen" voor je methode kan werken?
2. **Wat geef je mee als "gevraagde vibes"?** `Vibe`-objecten, hun `Id`'s of hun slugs? Wat moet je met elke keuze vergelijken?

## Stappen

- [ ] Merge opdracht 3 en maak een nieuwe feature branch vanaf `dev`
- [ ] Beslis de twee punten uit "Vooraf beslissen"
- [ ] Schrijf de code in Core
- [ ] Schrijf je testgevallen **op papier**, **op basis van de regel**. Kijk daarbij niet naar je code.
- [ ] Schrijf de tests en maak ze groen
- [ ] **Breek je code bewust**, bijvoorbeeld door `+ 1` bij de telling te zetten of door een voorwaarde om te draaien. Faalt er minstens één test? Zet daarna de code terug.
- [ ] **Refactor:** zowel de code als je tests
- [ ] `dotnet test` toont alle tests groen

## Nieuw in deze opdracht

**Tests achteraf schrijven.** Het risico ken je uit opdracht 2: wie de code al voor zich heeft, test makkelijk wat de code *doet* in plaats van wat de regel *zegt*. Zo'n test is altijd groen, ook als de code fout is.

Je hebt hiervoor twee vangnetten:

1. **Je testgevallen op papier**, vanuit de regel, net zoals in opdracht 3.
2. **De code bewust breken.** Blijven al je tests groen terwijl de code fout is, dan testen ze niet genoeg. Dit heet ook wel *mutation testing*, in dit geval met de hand.

## Om over na te denken (maximaal twee punten)

1. **`Vibe` is een `class`.** Dat besliste je in opdracht 1. Stel dat je twee `Vibe`-objecten met hetzelfde `Id` en dezelfde naam vergelijkt. Zijn die volgens C# gelijk? Wat betekent dat voor je keuze bij "Vooraf beslissen" punt 2?
2. **Randgevallen.** Wat verwacht je volgens de regel bij:
   - geen enkele gevraagde vibe;
   - een game zonder vibes;
   - dezelfde vibe die twee keer gevraagd wordt?

   Voor dat laatste geval zegt de regel niets letterlijks. Wat vind jij logisch, en hoe zou je dat met Kevin afstemmen?

## Klaar wanneer

- Alle tests zijn groen.
- `dotnet build` geeft geen warnings.
- Je kunt uitleggen welke test faalde toen je de code bewust brak, en waarom.
- Je kunt je keuze uit "Vooraf beslissen" punt 1 verdedigen.

## Buiten scope (komt later)

- **Opdracht 5:** een `MatchFinder` die alles combineert: ranking (vibe-overlap eerst, duurnabijheid als tiebreaker), top 3 en de Smart Time Stretch-limieten (max 2 korter, max 1 langer).
- Hoe de koppeling tussen games en vibes in de databank komt (EF Core-configuratie in Infrastructure).

## Documentatie

- [Unit testing C# with xUnit](https://learn.microsoft.com/dotnet/core/testing/unit-testing-csharp-with-xunit)
- [Equality comparisons (C#)](https://learn.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/equality-comparisons)
- [`Enumerable.Count`](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.count)
- [`Enumerable.Intersect`](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.intersect)
