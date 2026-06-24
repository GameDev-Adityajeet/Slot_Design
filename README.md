# 🎰 Slot Design - Unity Slot Machine Game

A browser-playable slot machine game developed in Unity using C#. The project focuses on clean architecture, modular design, smooth reel animations, audio feedback, and maintainable gameplay systems.

## 🌐 Play the Game

**WebGL Build:**
https://gamedev-adityajeet.itch.io/slot-design

---

## 📋 Assignment Overview

This project was developed as part of a Unity Game Development assignment to demonstrate:

* Unity gameplay programming fundamentals
* Object-Oriented Programming principles
* Random Number Generation (RNG) implementation
* UI/UX polish and animation
* ScriptableObject-based data management
* Audio integration
* WebGL deployment and browser compatibility

---

## 🎮 Game Overview

Slot Design is a classic 3-reel slot machine where players spend coins to spin reels and attempt to match symbols on the center row.

Each spin costs coins, and matching all three symbols rewards the player based on the symbol's payout value.

The game includes animated reels, win feedback, audio effects, balance management, and restart functionality.

---

## 🕹️ How to Play

1. Press the **SPIN** button.
2. Three reels begin spinning simultaneously.
3. Each spin costs **10 coins**.
4. Match all three symbols on the center row to win.
5. Payout depends on the matched symbol.
6. Continue playing until your balance reaches zero.
7. Press **Restart** to begin again with a fresh balance.

---

## 💰 Symbol Payout Table

| Symbol | Payout    |
| ------ | --------- |
| Cherry | 30 Coins  |
| Lemon  | 60 Coins  |
| Bar    | 90 Coins  |
| Bell   | 150 Coins |
| Seven  | 300 Coins |

---

## ✨ Features

### Core Gameplay

* 3-Reel Slot Machine System
* RNG-based result generation
* Dynamic payout calculation
* Coin balance management
* Win/Loss state handling

### Animation & Feedback

* Smooth reel scrolling animation
* Ease-in and ease-out reel movement
* Symbol landing punch effect
* Winning symbol pulse animation
* Animated win panel

### Audio

* Background music loop
* Spin sound effects
* Win sound effects
* Loss sound effects
* UI click feedback

### UI Systems

* Balance display
* Win panel
* Restart functionality
* Responsive layout

### Technical Features

* ScriptableObject-driven symbol configuration
* Singleton-based manager architecture
* Modular and reusable code structure
* WebGL deployment support

---

## 🏗️ Project Architecture

The project follows Object-Oriented Programming principles with clear separation of responsibilities.

### Core Systems

| Script          | Responsibility                                                                  |
| --------------- | ------------------------------------------------------------------------------- |
| SlotManager.cs  | Core gameplay logic, RNG, win detection, payout calculation, balance management |
| Reel.cs         | Reel spinning logic and symbol scrolling                                        |
| Symbol.cs       | Symbol display and visual feedback                                              |
| SymbolData.cs   | ScriptableObject data container for symbols                                     |
| UIManager.cs    | UI updates, panels, and button interactions                                     |
| AudioManager.cs | Centralized audio handling                                                      |

---

## 🧠 Technical Approach

### Architecture Design

The project follows a modular architecture where each script has a single responsibility.

The SlotManager acts as the central gameplay controller while UI and audio remain independent systems to reduce coupling and improve maintainability.

### RNG & Result Flow

The final result of a spin is determined before the reel animation begins.

Each reel receives its target symbol in advance and smoothly transitions to the predetermined result.

This guarantees:

* Consistent outcomes
* Accurate win detection
* No visual-result mismatch

### Animation System

The project combines Unity Coroutines and DOTween to create smooth and responsive animations.

Implemented effects include:

* Reel acceleration and deceleration
* Symbol landing punch
* Winning symbol pulse
* Win panel pop animation
* UI button interactions

### Audio Design

Audio is centralized through AudioManager.

Two AudioSource components are used:

* Background Music Source
* Sound Effects Source

This prevents sound effects from interrupting the music loop.

### Data-Driven Design

Symbol information is stored using ScriptableObjects.

Each symbol contains:

* Name
* Sprite
* Color
* Payout Value

This allows designers to modify gameplay values without changing code.

---

## 📂 Project Structure

```text
Assets/
├── Scripts/
├── Prefabs/
├── Animations/
├── UI/
├── Sounds/
├── ScriptableObjects/

WebGL_Build/
├── Build/
├── TemplateData/
└── index.html
```

---

## 🚀 Running the Project

### Unity Project

1. Clone the repository
2. Open using Unity 6 (6000.3.12f1)
3. Open the main scene
4. Press Play

### WebGL Build

The WebGL build is included inside:

```text
WebGL_Build/
```

To run locally:

#### Option 1 - Python Server

```bash
python -m http.server 8000
```

Open:

```text
http://localhost:8000
```

#### Option 2 - VS Code Live Server

1. Install Live Server extension
2. Open the WebGL_Build folder
3. Right-click index.html
4. Select "Open with Live Server"

---

## 🛠️ Built With

* Unity 6 (6000.3.12f1)
* C#
* DOTween
* TextMeshPro
* WebGL

---

## 🎯 Design Goals

The primary goals of this project were:

* Create a polished slot machine gameplay loop
* Demonstrate clean Unity architecture
* Showcase reusable and maintainable code
* Implement data-driven gameplay systems
* Deliver a browser-playable WebGL experience

---

## 👨‍💻 Developer

**Adityajeet Yadav**

Game Developer | Unity Developer | Unreal Engine Developer

Portfolio:
https://gamedev-adityajeet.github.io/Portfolio/

GitHub:
https://github.com/GameDev-Adityajeet

---

## 📌 Submission Notes

This repository includes:

✔ Full Unity Project Source Code

✔ Complete WebGL Build

✔ Documentation and Technical Breakdown

✔ Meaningful Git Commit History

✔ Organized Project Structure

✔ Browser Playable Version

WebGL Demo:
https://gamedev-adityajeet.itch.io/slot-design
