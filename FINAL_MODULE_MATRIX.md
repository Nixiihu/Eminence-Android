
# Eminence Final Max — Module Matrix

## Startup defaults

Everything that can affect gameplay starts OFF.

Profile: Legit
Anti-cheat policy: Flag Only
Aim target: Head
Aim Assist: OFF
Scope test: OFF
No Recoil: OFF
ESP: OFF
Boxes: OFF
Skeleton: OFF
Fast Switching: OFF
Lead Aim: OFF

## Aim Assist

Functional after the game's own camera and target transforms are assigned.

Behavior:
- Does not move the camera while input is inactive.
- Activates only while configured aim/fire or scope input is held.
- Checks target validity.
- Checks FOV.
- Applies reaction delay.
- Limits turn speed.
- Uses smoothing/drag.
- Supports Head/Neck/Body.
- Adds configurable test error.

## Scope

The harness can use the scope input gate. A complete scope implementation must be connected to the game's own scope state/API.

## No Recoil

Functional after the game's weapon test code calls RegisterRecoil().
Uses configurable compensation rather than modifying arbitrary game memory.

## ESP

Functional as a development visualization using transforms supplied by the game.
Boxes and target markers work.
A true bone-by-bone skeleton needs the game's actual bone transforms.

## Fast Switching

Functional test sequence with profile-specific timing.
It does not manipulate an external game's weapon inventory.

## Lead Aim

Functional mathematical prediction:
predictedPosition = targetPosition + targetVelocity * travelTime * multiplier

travelTime is derived from distance / projectile speed.

## Details

FPS and frame time are available.
CPU/GPU/RAM/ping should be supplied by the game's telemetry provider for accurate device-specific values.

## Mini GUI

Recommended as a Unity Canvas inside the development build:
- tiny Eminence button
- one-tap open
- draggable
- safe-area aware
- responsive scaling
- no separate process
- no Android overlay permission required

## Stability

The bootstrap:
- runs only in Unity Editor/Development Build
- initializes modules OFF
- catches initialization failures
- disables itself when required references are missing
- does not create an external process
- does not inject or patch another process

## Detection

The harness is not designed to hide from Android, the game, or anti-cheat. The correct way to prevent test results from being confused with production behavior is to use a development/test build and an explicit Flag Only/Log Only policy.
