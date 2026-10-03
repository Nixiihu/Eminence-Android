# Eminence v3 — Complete Preview

## Overall flow

1. Start your Unity development build.
2. The Eminence test components are loaded as normal development-scene components.
3. No separate overlay window is required.
4. Triple-tap can open the development panel.
5. Select Legit or Rage.
6. Enable a module.
7. The module generates controlled behavior using your game's own development objects.
8. Your anti-cheat receives/records the resulting telemetry.

## Aim Assist — actual behavior

Default:
- Disabled.
- Head target is selected by default.
- No autonomous target seeking.

When enabled:
- Player must actively hold the configured aim/fire input.
- The target must be inside the configured FOV.
- Reaction delay is applied.
- Camera rotation is limited by max turn speed.
- Smoothness and drag control response.
- A small configurable error is introduced.
- Releasing the input stops correction immediately.

Therefore it behaves as an input-driven test modifier rather than an autonomous camera.

## Scope

Scope input can activate the same input gate through Fire2. For a complete game-specific scope test, connect the component to the game's own scope state.

## No Recoil

Your weapon test calls RegisterRecoil(pitch, yaw). The harness applies the configured amount of counter-rotation. Legit uses restrained compensation; Rage uses full compensation.

## Lead Aim

Predicts:

target position + target velocity × projectile travel time × prediction multiplier

Projectile travel time is derived from distance / projectile speed.

## Fast Switching

The harness produces controlled weapon-switch sequences.

Legit:
- slower interval
- fewer switches

Rage:
- very short interval
- many switches

This is intended to create distinct anti-cheat telemetry.

## ESP

The development renderer can show:
- target markers
- boxes
- target names
- test skeleton indicator

For a real skeleton visualization, connect head/body/limb bone transforms from your own player prefab.

## Details

Shows:
- FPS
- frame time

Additional CPU/GPU/RAM/ping values should come from your game's own telemetry provider rather than guessed values.

## Triple Tap

Three taps inside the configured interval toggle the development panel.

## Anti-cheat behavior

Recommended development setup:

FLAG ONLY
    ↓
Eminence test behavior
    ↓
Your anti-cheat detects it
    ↓
Record module + telemetry
    ↓
No punishment

This lets you tune detection thresholds safely.

## What "background" means here

The harness can run as normal Unity components without opening a separate operating-system window.

It does NOT:
- inject into another process
- hide itself from security software
- bypass anti-cheat
- conceal telemetry
- modify another game's memory

That distinction keeps the harness appropriate for development and anti-cheat testing.

## Functional status

Aim Assist: FUNCTIONAL after camera/target references are assigned.
Input gating: FUNCTIONAL.
No Recoil: FUNCTIONAL after weapon test code supplies recoil.
Lead Aim: FUNCTIONAL.
Fast Switching: FUNCTIONAL test sequence.
ESP: FUNCTIONAL development visualization.
Skeleton: visualization requires bone references.
Details FPS/frame timing: FUNCTIONAL.
Triple Tap: FUNCTIONAL.
Anti-cheat Flag/Log policy: FUNCTIONAL as a development test policy.
Game-specific scope integration: REQUIRES YOUR GAME'S SCOPE API.
CPU/GPU/RAM/ping telemetry: REQUIRES YOUR GAME/telemetry provider for accurate values.
