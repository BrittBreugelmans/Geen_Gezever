# 03 - Business & UI Rules

## 1. Shelf Interaction ("Wiggle Mode")
- **Trigger:** Long-press ($\ge 500\text{ ms}$) on any game card or explicit "Edit" button.
- **Behavior:** All game cards initiate subtle CSS rotation shake (`@keyframes wiggle`).
- **Actions in Mode:** Displays a delete badge (`✕`) on each item and transforms the primary navigation action to "Done".

## 2. Match Finder Filter Rules
- **Hard Filter:** `MinPlayers <= RequestedPlayers AND MaxPlayers >= RequestedPlayers`. Non-matching player counts are strictly discarded.
- **Soft Filters:** Duration proximity and Vibe matching determine ranking scores.
- **Empty Match Fallback (Smart Time Stretch):**
  - Triggers when exact duration yields 0 matches.
  - Returns up to 2 matches where duration is $\le 15$ min shorter and 1 match where duration is $\le 15$ min longer.
  - Player count constraint remains strictly enforced.

## 3. Tinder Swipe Mode Rules
- The deck combines exact matches with games within $\pm 15$ minutes of target playtime to guarantee interactive deck depth.