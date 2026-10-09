# Vragen voor Kevin

## Open

### 1. BGG zonder spelersaantal

*Opdracht 1, `Game.cs`*

Wat als BGG geen spelersaantal heeft, of `0` teruggeeft?

- Context: `MinPlayers` en `MaxPlayers` worden ingevuld met de gegevens van BGG. De spelersfilter is een harde regel: `MinPlayers <= aantal <= MaxPlayers`.
- Opties:
  - De properties nullable maken (`int?`). Dan moet elke plek die de filter gebruikt rekening houden met `null`.
  - `int` houden en het randgeval apart afhandelen, bijvoorbeeld bij het importeren van BGG-gegevens.
- Bijvraag: moet een game zonder spelersaantal dan nooit, altijd of op een andere manier aanbevolen worden?

## Beantwoord

<!-- Verplaats vragen hierheen met het antwoord van Kevin -->
