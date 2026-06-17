# MazeRunner
A physics-based mobile maze game built with Unity 6 LTS — my first ever Unity project.

---

## About

MazeRunner is a 3D mobile maze game where the player controls a ball navigating from a Start zone to a Finish zone through a maze with 8 distinct paths. Built entirely from scratch with no prior Unity or C# experience.

The goal was to learn Unity fundamentals hands-on by building a complete, playable game — from maze generation to Android deployment.

---

## Gameplay

- Roll the ball from the **green Start zone** to the **red Finish zone**
- Avoid **pink spinning obstacles** — each hit costs 100 points and respawns you
- Reach the **yellow Checkpoint** to save your progress mid-maze
- Beat the maze as fast as possible for the highest score
- Top 5 scores saved locally between sessions

---

## Features

- **Physics-based movement** — Rigidbody.AddForce for natural rolling behavior
- **8 navigation paths** — multiple routes through the maze, each with different risk/reward
- **Procedural maze construction** — maze built programmatically from a grid array via Unity Editor script
- **AI obstacle** — enemy cube patrols the fastest path, blocking the direct route
- **Checkpoint system** — saves position mid-maze, respawns there on death
- **Dual camera modes** — fixed top-down overview and smooth follow camera, switchable anytime
- **Scoring system** — starts at 1000, decreases with time and obstacle penalties
- **Top 5 local leaderboard** — persisted between sessions using PlayerPrefs
- **Full UI** — Start, Pause, Win, and Lose screens with sensitivity adjustment
- **Cross-platform input** — keyboard on PC, on-screen D-pad on Android

---

## Controls

### Android
- **D-Pad (bottom-left)** — Move the ball
- **CAM button (top-right)** — Switch camera mode
- **PAUSE button (top-right)** — Pause game

### PC
- **WASD / Arrow Keys** — Move the ball
- **C** — Switch camera mode
- **ESC** — Pause game

---

## Technical Highlights

### Grid-Based Maze Generation
The maze is defined as a 27×27 integer grid (0 = wall, 1 = corridor) in a Unity Editor script. Wall positions are calculated mathematically from grid indices, centering the maze at world origin. This approach meant the designed layout exactly matched the built result — no manual coordinate errors.

### Physics Movement
Movement uses Rigidbody.AddForce in FixedUpdate rather than setting transform position directly. This produces natural rolling physics and frame-rate independent behavior across all devices.

### Programmatic UI
All UI panels, buttons, text, and sliders are built entirely in code using a builder pattern — no manual Unity Inspector setup. Canvas uses ScaleWithScreenSize for consistent layout across all Android screen resolutions.

### Singleton Pattern
GameManager, UIManager, and MobileJoystick use the singleton pattern with duplicate destruction in Awake, ensuring only one instance exists across scene reloads.

### Local Leaderboard
Top 5 scores stored in PlayerPrefs using indexed keys. On each game end, scores are inserted into an array, sorted descending, and the top 5 written back — persisted between sessions.

---

## What I Learned

This was my first Unity project. Going in with zero knowledge of Unity or C#, I learned:

- Unity's component-based architecture and scene management
- Physics-based movement with Rigidbody
- Trigger and collider systems for game events
- Unity's new Input System for cross-platform input
- Programmatic UI construction with Canvas and RectTransform
- Game state management with singletons
- Android build pipeline and real device testing
- Debugging platform-specific issues (input system conflicts, DontDestroyOnLoad duplicates)

---

## Built With

- Unity 6 LTS
- C#
- Android SDK

---

## Download

APK available in the [Releases](../../releases) section.
