# Technical Architecture

## Engine

-   Godot 4.x
-   C#
-   Android first, iOS second

## Project Structure

Assets/ Scenes/ Scripts/ Autoload/ Data/ Resources/ UI/

## Core Managers

-   GameManager
-   SaveManager
-   MonsterManager
-   BattleManager
-   BluetoothManager
-   AudioManager
-   EventBus

Managers communicate through events rather than direct references.

## Monster Model

Monster - GUID - Species - Stage - Stats - Traits - Genetics -
Personality - Inventory - BattleHistory

## Save Format

JSON with versioning. Cloud sync optional.

## Bluetooth

Use Bluetooth LE. Exchange: - Monster snapshot - Random seed - Protocol
version

Each client simulates identical deterministic battle locally.

## Coding Standards

-   SOLID principles
-   Dependency injection where practical
-   Data-driven resources
-   UI separated from logic
-   Small reusable systems
-   Unit-test deterministic battle calculations

## Roadmap

Milestone 1: Single-player virtual pet. Milestone 2: Evolution system.
Milestone 3: Bluetooth battles. Milestone 4: Online backend. Milestone
5: Live events.
