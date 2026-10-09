# 03 - Business & UI Rules

## 1. Shelf Interaction ("Wiggle Mode")
- **Trigger:** Long-press ($\ge 500\text{ ms}$) on any game card or explicit "Edit" button.
- **Behavior:** All game cards initiate subtle CSS rotation shake (`@keyframes wiggle`).
- **Actions in Mode:** Displays a delete badge (`✕`) on each item and transforms the primary navigation action to "Done".

## 2. Match Finder Filter Rules
- **Hard Filter:** `MinPlayers <= RequestedPlayers AND MaxPlayers >= RequestedPlayers`. Non-matching player counts are strictly discarded.
- **Exact Duration Match:** `MinPlayTimeMinutes <= RequestedDuration AND MaxPlayTimeMinutes >= RequestedDuration`. Example: a request for 60 min matches a 45–90 min game.
- **Ranking:**
  1. Vibe match first: more overlap between the requested vibes and the game's vibes ranks higher.
  2. Duration proximity only as a tiebreaker: the distance between the requested duration and the midpoint of the game's range. Smaller is better.
- **Empty Match Fallback (Smart Time Stretch):**
  - Triggers when exact duration yields 0 matches.
  - Returns up to 2 shorter games (`RequestedDuration - 15 <= MaxPlayTimeMinutes < RequestedDuration`) and up to 1 longer game (`RequestedDuration < MinPlayTimeMinutes <= RequestedDuration + 15`).
  - Example for 60 min: a 50–55 game counts as shorter, a 70–80 game counts as longer.
  - Within each group, the ranking above applies.
  - Player count constraint remains strictly enforced.

## 3. Tinder Swipe Mode Rules
- The deck combines the exact matches with all shorter and longer games within the 15-minute bounds of the fallback, to guarantee interactive deck depth.