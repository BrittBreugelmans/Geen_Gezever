# Opdracht 2: De spelersfilter, met unit tests

## Doel

Bouw de eerste business rule in Core: de **harde spelersfilter**, met unit tests die bewijzen dat hij werkt.

> `MinPlayers <= RequestedPlayers AND MaxPlayers >= RequestedPlayers`
> Zie [03-business-rules.md](../03-business-rules.md) en Principe IV in de constitution.

Je oefent hier twee nieuwe dingen: een testproject opzetten en werken volgens **Red → Green → Refactor**.

## Stappen

- [X] Maak een feature branch vanaf `dev`
- [X] Maak een testproject aan (zie "Vooraf beslissen")
- [X] Voeg het testproject toe aan de solution
- [X] Geef het testproject een referentie naar Core
- [X] **Red:** schrijf je eerste test, die faalt (of zelfs niet compileert)
- [X] **Green:** schrijf net genoeg code in Core om die test te laten slagen
- [X] Herhaal Red → Green voor je andere testgevallen
- [X] **Refactor:** maak je code netter terwijl alle tests groen blijven
- [X] `dotnet test` toont alle tests groen

## Vooraf beslissen

- **Testframework:** de constitution legt er geen vast. Het meest gangbare in .NET is **xUnit**, en de Microsoft-docs gebruiken het ook. Neem dat voor nu en vraag Kevin wat ze bij hen gebruiken.
- **Naam van het testproject:** kijk naar de naam van je andere projecten en kies iets wat daarbij past.

## Hints voor de commando's

Je hebt `dotnet new`, `dotnet sln` en `dotnet add` nodig. Zoek zelf de juiste argumenten op met `--help`, bijvoorbeeld `dotnet sln --help`. Zo leer je de CLI lezen zonder dat je iets moet onthouden.

## Om over na te denken (maximaal twee punten)

1. **Waar in Core, en hoe noem je het?** Een methode op `Game`? Een aparte klasse? Wat leest straks het duidelijkst als de duurfilter en de vibe-scoring erbij komen?
2. **Welke testgevallen?** Bedenk er minstens vier. Een tip: bugs zitten meestal op de **grenzen**. Wat als het aantal spelers precies gelijk is aan `MinPlayers`? En precies aan `MaxPlayers`?

## Klaar wanneer

- Alle tests zijn groen.
- `dotnet build` geeft geen warnings.
- Je kunt uitleggen waarom je eerst een falende test schrijft.

## Documentatie

- [Unit testing C# with xUnit](https://learn.microsoft.com/dotnet/core/testing/unit-testing-csharp-with-xunit)
- [`dotnet sln`](https://learn.microsoft.com/dotnet/core/tools/dotnet-sln)
- [`dotnet add reference`](https://learn.microsoft.com/dotnet/core/tools/dotnet-add-reference)
