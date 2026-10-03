
# Eminence Android Development Setup

## Recommended architecture

For a Unity development build, put the Eminence mini button and full panel inside the game's Unity UI.

Flow:

Select development game
→ Launch
→ Game loads
→ all test modules OFF
→ tiny Eminence button appears
→ tap button
→ full GUI opens
→ explicitly enable a test module

This avoids a separate Android overlay process.

## Mini button

Recommended:
- 44–56 dp touch target
- draggable
- safe-area aware
- persists its position locally
- no gameplay behavior while closed
- opens the development panel with one tap

## Android safety

Use a normal Canvas + EventSystem. Do not request overlay/accessibility/root permissions just to display the development UI.

The harness does not create a second process and does not inject into another application.

## Important

This package does not contain Android stealth/evasion logic and cannot guarantee that an anti-cheat will not identify a development harness. For reliable testing, use a development build and explicitly allow the harness in the game's test configuration.
