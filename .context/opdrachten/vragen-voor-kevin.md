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

### 2. Herkomst van de vibe-tags

*Opdracht 4, `GameVibe`*

Waar komen de vibes van een game (`GameVibe`-tags) vandaan?

- Context: de documentatie zegt niet wie de koppeling tussen een game en zijn vibes legt. BGG kent categorieën en mechanics, maar geen vibes zoals "gezellig" of "chaotisch".
- Opties:
  - De gebruiker tagt zelf, bijvoorbeeld bij het toevoegen van een game aan de kast.
  - Een vaste lijst die wij (of een beheerder) per game invullen.
  - Afleiden uit BGG-categorieën of mechanics via een vertaaltabel.
- Bijvraag: als een game (nog) geen vibes heeft, scoort hij 0 op vibe-overlap en komt hij dus lager in de ranking. Is dat de bedoeling?

### 3. Dezelfde vibe twee keer gevraagd

*Opdracht 4, `Game.VibeMatchCount`*

Wat als dezelfde vibe twee keer in de gevraagde vibes zit?

- Context: de regel zegt "the number of requested vibes that the game has". Hij zegt niet wat er gebeurt bij een dubbel gevraagde vibe. Op dit moment telt `VibeMatchCount` die vibe twee keer als de game ze heeft, en zo krijgt die vibe meer gewicht in de ranking.
- Opties:
  - Dubbels tellen gewoon mee, zoals nu.
  - Dubbels tellen maar één keer. De methode houdt daar zelf rekening mee.
  - Het kan in de praktijk niet voorkomen, bijvoorbeeld omdat de UI met aanvinkvakjes werkt. Dan is het geen probleem van de methode, maar moet dat wel ergens vastgelegd worden.
- Opvolging: zodra dit beslist is, wordt het gedrag vastgelegd met een test.

## Beantwoord

<!-- Verplaats vragen hierheen met het antwoord van Kevin -->
