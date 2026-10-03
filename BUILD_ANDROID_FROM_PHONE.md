# Build Eminence Android APK from a phone

This is a complete Unity project wrapper around the existing Eminence test-harness scripts.

Unity version: 2022.3.45f1
Scene: Assets/Scenes/EminenceDemo.unity

The project is a development/test harness. It does not inject into another app, patch memory, or implement anti-detection/stealth behavior.

For cloud building, upload this project to a Git repository and use a Unity-capable CI service such as GameCI/GitHub Actions. You still need a valid Unity license/activation for the build runner.

For a local PC build, open the project in Unity 2022.3.45f1, open the demo scene, then File > Build Settings > Android > Build.
