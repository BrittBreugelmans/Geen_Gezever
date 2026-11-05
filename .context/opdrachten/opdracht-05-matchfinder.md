# Opdracht 5: De MatchFinder, alles samen

## Doel

Bouw de `MatchFinder`: de logica die uit een lijst games de **top 3** kiest voor een vraag ("met hoeveel spelers, hoe lang, welke vibes?").

In opdracht 2 tot 4 bouwde je losse regels op `Game`. Nu combineer je ze. Dat is ook het verschil met de vorige opdrachten: een `Game` kent enkel zichzelf, maar de `MatchFinder` moet over **meerdere** games tegelijk redeneren.

De opdracht bestaat uit twee delen. Werk ze na elkaar af.

Probeer zo ver mogelijk te geraken zonder hulp, en noteer waar je vastliep.

## De regels

Uit de constitution (Matching Engine Specification, regels 2 tot 6) en [03-business-rules.md](../03-business-rules.md). `d` is de gevraagde duur in minuten.

### Deel A: filter, ranking en top 3

1. **Hard filter:** enkel games die het gevraagde aantal spelers ondersteunen. Geen uitzonderingen (Principe IV).
2. **Exacte duurmatch:** enkel games waarbij `d` binnen hun speelduur valt.
3. **Ranking:**
   1. **Vibe-overlap** eerst: meer overlap staat hoger.
   2. **Duurnabijheid**, enkel als tiebreaker: de afstand tussen `d` en het midden van de speelduur, `(MinPlayTimeMinutes + MaxPlayTimeMinutes) / 2`. Een kleinere afstand staat hoger.
4. **Output:** de top 3 (Principe V).

Voorbeeld bij `d = 60`, alle games ondersteunen het aantal spelers:

| Game | Speelduur | Vibe-overlap | Midden | Afstand tot 60 | Plaats |
|---|---|---|---|---|---|
| A | 45–90 | 2 | 67 | 7 | 2 |
| B | 50–70 | 2 | 60 | 0 | 1 |
| C | 30–60 | 1 | 45 | 15 | 3 |
| D | 60–120 | 0 | 90 | 30 | valt buiten de top 3 |

A en B hebben evenveel overlap, dus beslist de afstand. D wordt niet uitgesloten door zijn 0 overlap, maar valt af omdat er maar drie plaatsen zijn.

### Deel B: Smart Time Stretch

5. **Fallback:** als er **geen enkele** game exact matcht op duur, geef je in de plaats:
   - maximaal **2 kortere** games (`d - 15 <= MaxPlayTimeMinutes < d`);
   - maximaal **1 langere** game (`d < MinPlayTimeMinutes <= d + 15`).

   Binnen elke groep geldt de ranking uit regel 3. De spelersfilter blijft gewoon gelden.

## Vooraf beslissen

Dit zijn jouw keuzes. Noteer wat je kiest en waarom.

1. **Waar en in welke vorm komt de `MatchFinder`?** Het is domeinlogica, dus ze hoort in Core (zie [01-architecture.md](../01-architecture.md)). Maar in welke map, en als wat? Een gewone class die je aanmaakt met `new`, een `static` class, iets anders? Denk eraan dat de API de `MatchFinder` later moet kunnen gebruiken.
2. **Hoe geef je de vraag mee?** Spelers, duur en vibe-Id's als losse parameters, of samen in één object? Kies je voor een object, wordt dat dan een `class` of een `record`? Denk terug aan wat je in opdracht 4 leerde over gelijkheid.
3. **Wat geef je terug?** Welk collectietype? En moet degene die de `MatchFinder` aanroept kunnen weten of het resultaat een exacte match is of een fallback? De UI zal dat later willen tonen.

## Stappen

- [ ] Commit en merge opdracht 4, en maak een nieuwe feature branch vanaf `dev`
- [ ] Beslis de drie punten uit "Vooraf beslissen"
- [ ] Schrijf per regel je testgevallen **op papier**, vanuit de regel
- [ ] **Deel A:** filter, ranking en top 3, met tests
- [ ] **Deel B:** de fallback, met tests
- [ ] **Breek je code bewust**, zoals in opdracht 4. Faalt er minstens één test?
- [ ] **Refactor:** zowel de code als je tests
- [ ] `dotnet test` toont alle tests groen

Je kiest zelf of je test-driven werkt (opdracht 2 en 3) of eerst de code schrijft (opdracht 4). Noteer welke aanpak je kiest en waarom.

## Nieuw in deze opdracht

**Hergebruik.** Je hebt de spelers- en duurregels al als methodes op `Game`, en de vibe-overlap ook. De `MatchFinder` hoeft die regels dus niet opnieuw te schrijven. Hij moet ze **aanroepen** en combineren.

**LINQ.** Filteren, sorteren en de eerste drie nemen kan met `foreach`-lussen, maar sorteren met de hand wordt snel lang en foutgevoelig. In .NET gebruik je daar meestal LINQ voor. Bekijk zeker `Where`, `OrderByDescending`, `ThenBy` en `Take` (links onderaan). Let goed op het verschil tussen `ThenBy` en een tweede `OrderBy`.

**Testdata.** Je tests hebben nu een **lijst** games nodig, telkens met een speelduur, spelersaantal en vibes. Denk na over helpers die dat opbouwen, zodat elke test leesbaar blijft. Je helpers uit opdracht 3 en 4 kunnen je op weg zetten.

## Om over na te denken (maximaal twee punten)

1. **Het midden van de speelduur.** Bij game A in de tabel is `(45 + 90) / 2` volgens C# `67`, niet `67.5`. Waarom? Kan dat ooit tot een andere volgorde leiden dan je zou verwachten? Zo ja, wat vind jij dan juist?
2. **Wat de regels niet zeggen.** Twee games met dezelfde overlap én dezelfde afstand: wie komt eerst? En in de fallback: komen de kortere games voor de langere, of omgekeerd, of door elkaar volgens de ranking? Beslis wat je logisch vindt, en noteer wat je aan Kevin wilt voorleggen in [vragen-voor-kevin.md](vragen-voor-kevin.md).

## Klaar wanneer

- Alle tests zijn groen.
- `dotnet build` geeft geen warnings.
- Je kunt een test aanwijzen die faalt als duurnabijheid **meer** zou wegen dan vibe-overlap, en uitleggen waarom.
- Je kunt je keuzes uit "Vooraf beslissen" verdedigen.

## Buiten scope (komt later)

- **De kandidaten:** enkel de `UserGame`-exemplaren van de bronnen die de host voor vanavond actief zette (Principe III). Voorlopig krijgt de `MatchFinder` gewoon een lijst games mee.
- **Tinder swipe mode** (regel 7).
- De games uit de databank halen, inclusief hun `GameVibes` (repository en `Include` in Infrastructure).
- Een API-endpoint en DTO's voor de Match Finder.
- Wat de UI toont als ook de fallback niets oplevert.

## Documentatie

- [Language Integrated Query (LINQ)](https://learn.microsoft.com/dotnet/csharp/linq/)
- [Sorting data (LINQ)](https://learn.microsoft.com/dotnet/csharp/linq/standard-query-operators/sorting-data)
- [`Enumerable.Where`](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.where)
- [`Enumerable.Take`](https://learn.microsoft.com/dotnet/api/system.linq.enumerable.take)
- [`Math.Abs`](https://learn.microsoft.com/dotnet/api/system.math.abs)
- [Integer division](https://learn.microsoft.com/dotnet/csharp/language-reference/operators/arithmetic-operators#integer-division)
- [Records (C#)](https://learn.microsoft.com/dotnet/csharp/fundamentals/types/records)
- [Static classes (C#)](https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members)
