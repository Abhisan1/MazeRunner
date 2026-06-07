# MazeRunner
**Game Developer Intern Assessment — Reliance Games x Zapak**
**Built with Unity 6 LTS | Android APK**

---

## Project Overview

MazeRunner is a physics-based mobile maze game where the player controls a sphere navigating from a green Start Zone to a red Finish Zone. The maze contains 8 distinct navigation paths, 3 spinning static obstacles, 1 AI-patrolling obstacle, and a checkpoint system. The game is fully playable on both Android and PC.

---

## Controls

### Android
- **D-Pad buttons (bottom-left)** — Move the ball (Up/Down/Left/Right)
- **CAM button (top-right)** — Switch between Top-Down and Follow camera
- **PAUSE button (top-right)** — Pause the game

### PC (Unity Editor)
- **WASD / Arrow Keys** — Move the ball
- **C** — Switch camera mode
- **ESC** — Pause game

### Sensitivity
Adjustable via the slider on the Start Screen before the game begins.

---

## How to Play

1. Launch the game and adjust sensitivity if needed
2. Press **PLAY**
3. Navigate the ball from the **green Start Zone** (left) to the **red Finish Zone** (right)
4. Avoid **pink spinning obstacles** — each hit deducts 100 points and respawns the ball
5. Cross the **yellow Checkpoint** in the center — if you die after this, you respawn at the checkpoint instead of the start
6. Reach the Finish Zone to Win
7. Score is based on completion speed and number of penalties

---

## Technical Decisions

### Maze Generation via Editor Script
The maze is built programmatically using a Unity Editor script (MazeGenerator.cs). The layout is defined as a 27×27 integer grid where 0 = wall and 1 = open corridor. Wall positions are calculated mathematically from grid indices, guaranteeing the built maze exactly matches the designed layout. This approach demonstrates editor tooling knowledge and eliminates manual coordinate errors.

### Physics-Based Movement
Player movement uses Rigidbody.AddForce() rather than directly setting transform position or velocity. This produces natural rolling behavior and realistic wall collisions. Rotation is frozen on all axes to prevent the ball from tipping.

### Mobile Input — D-Pad over Accelerometer
Accelerometer input was implemented and tested first. However, the ball required extreme phone tilt to produce meaningful movement, creating poor user experience. The decision was made to switch to on-screen D-pad buttons, which provide precise, comfortable control on all Android devices. This is a deliberate UX-driven engineering decision, not a shortcut.

The D-pad is built programmatically via DpadSetup.cs using Unity's EventTrigger system, which handles press-and-hold reliably across all Android versions.

### Dual Camera System
Two camera modes implemented in CameraController.cs:
- **Top-Down (default):** Fixed overhead view at height 35 (PC) or 45 (mobile), centered on the maze. Player can see the full layout and plan their route.
- **Follow Camera:** Smooth third-person view using Vector3.Lerp, looks at player position via LookAt(). More immersive for navigation.

Switching is handled by toggling a boolean in LateUpdate() each frame. On PC press C, on Android tap the CAM button.

### Checkpoint State Management
The checkpoint stores two values — a boolean (reached?) and a Vector3 (world position). On obstacle collision, GameManager checks this state and respawns at checkpoint or start accordingly. This is a minimal but complete state machine.

### Scoring System
Score starts at 1000 and decreases at 10 points per second. Each obstacle collision deducts 100 points immediately. Final score is saved on both Win and Lose. This rewards fast, clean runs.

### Local Leaderboard
Top 5 scores stored in PlayerPrefs using indexed keys (Score_0 through Score_4). On each game end, the new score is inserted, array sorted descending, and top 5 written back. Displayed on the Win screen.

### UI Architecture
All UI is built entirely in code via UIManager.cs using a builder pattern (MakePanel, AnchoredText, AnchoredButton, AnchoredSlider). No manual Unity Inspector UI setup required. Canvas uses ScaleWithScreenSize at 1920×1080 reference resolution for consistent layout across all screen sizes.

---

## Challenges Faced

**1. Unity 6 New Input System**
Unity 6 uses the new Input System by default, which broke legacy Input.GetAxis() calls. Required switching to Keyboard.current from UnityEngine.InputSystem throughout all scripts, and setting Active Input Handling to "Both" in Player Settings to maintain compatibility.

**2. Maze Coordinate Accuracy**
Early maze versions used hand-written wall coordinates that did not match the visual layout. Solved by switching to a 2D grid array where positions are calculated mathematically — same source data drives both the visual diagram and the Unity build.

**3. Android Touch Input**
Multiple approaches were attempted for mobile input — accelerometer, Unity new Input System touch API, legacy Input.touches. Each had platform-specific compatibility issues with Unity 6's input handling. The final solution was D-pad buttons using Unity's EventTrigger (IPointerDownHandler, IPointerUpHandler) which works reliably on all Android devices regardless of Input System configuration.

**4. DontDestroyOnLoad Causing Duplicate Objects**
When the scene reloaded on restart, DontDestroyOnLoad caused duplicate canvases, EventSystems, and player balls to stack up. Fixed by removing DontDestroyOnLoad from runtime-created objects and adding singleton duplicate-destruction logic in Awake().

**5. UI Scaling Across Screen Sizes**
Fixed pixel UI coordinates broke on different Android resolutions. Solved by switching Canvas Scaler to ScaleWithScreenSize mode with reference resolution 1920×1080 and using normalized anchor positions (0.0–1.0) instead of fixed pixel offsets.

---

## Improvements With Additional Time

- **Procedural maze generation** — recursive backtracking algorithm for infinite unique layouts
- **Multiple difficulty levels** — faster AI, more obstacles, narrower corridors
- **Sound effects** — collision sounds, checkpoint chime, background music
- **Particle effects** — dust trail on movement, explosion on obstacle collision
- **Haptic feedback** — vibration on obstacle collision and checkpoint reached
- **Global leaderboard** — cloud-based scores using Unity Gaming Services
- **Level progression** — multiple mazes unlocked as player completes each one
- **Ball skin selection** — cosmetic customization on Start Screen
