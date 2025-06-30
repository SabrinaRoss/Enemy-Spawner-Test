# Enemy Spawn System - Unity Job Test

## Overview
This project is a simple 3D Unity game that implements an enemy spawn system with four spawn points. At runtime, the game selects a random time of day and adjusts enemy spawn behavior accordingly. Enemies spawn based on defined spawn rates and classes.

## Features
- **Four Spawn Points** spawning specific enemy types or classes:
  - Spawn Point 1: Archers only
  - Spawn Point 2: Grunts only
  - Spawn Point 3: Red enemies only
  - Spawn Point 4: Any enemy
- **Enemy Classes:** Grunts, Archers, Assassins
- **Enemy Types:** Red, Brown, Blue, Green, Yellow --> each with attributes for attack power, health, speed, and spawn rate
- **Day Cycle:** Morning, Afternoon, Night --> each affecting spawn rates and enemy stats differently
- **Dynamic Time of Day:** The time of day changes gradually during gameplay, affecting spawn behavior
- Enemies are represented as primitive shapes with color coding and scaled down for better visibility
- Added Rigidbody component to enemies for gravity effect at runtime
- Spawn locations randomized within a radius around each spawn point

## How to Run
1. Open the project recomended in Unity 60000.0.51f1 LTS.
2. Run the SampleScene in the Assets/Scene folder.
3. Press Play to start the scene.
4. Observe enemies spawning from the designated spawn points.
5. The directional light simulates time of day changing dynamically.

## Scripts Summary
- **SpawnerScript:** Handles enemy spawning logic, spawn points, and applying time-of-day effects.
- **TimeOfDayScript:** Manages the time of day cycle and directional light rotation.
- **EnemyScript:** Defines enemy attributes and class.

## Project Structure
- **Assets/Scripts:** Contains all C# scripts.
- **Assets/Materials:** Contians the material for the enemies colour.
- **Scene:** Basic environment with four spawn point game objects.

