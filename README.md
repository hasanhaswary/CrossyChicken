# 🐔 Crossy Chicken

[![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-blue.svg)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Status](https://img.shields.io/badge/Status-In%20Development-yellow.svg)](STATUS)
[![Platform](https://img.shields.io/badge/Platform-PC-orange.svg)](PLATFORM)

## 📖 Overview

A modern take on the classic Crossy Road game built with Unity! Guide your chicken across various terrains including grass, roads, rivers, and railways. Dodge cars and trains, hop on logs to cross rivers, and see how far you can go. Challenge yourself to reach the target score and complete each level!

## ✨ Features

- 🎮 Simple and intuitive controls with WASD or arrow keys
- 🚗 Dynamic obstacle spawning - cars, trains, and floating logs
- 🌊 Multiple terrain types - grass, roads, rivers, and railways
- 🏆 Score-based level progression
- 🎯 Challenge yourself to reach the target score
- 🔊 Immersive sound effects and visual feedback
- 💀 Watch out for hazards - falling in water, getting hit by vehicles, and more!

## 🎮 Controls

- **Arrow Keys** / **WASD**: Move the chicken (Up, Down, Left, Right)

## 🛠️ Technical Details

Built with Unity, this game features:

- Procedural world generation system
- Dynamic obstacle spawning with variable difficulty
- Advanced collision detection and physics interactions
- Parent-child transform relationships for river log mechanics
- Singleton pattern for difficulty management
- Scene management for different game modes
- Particle effects for death and level completion
- Terrain-specific behavior management

## 🚀 Getting Started

### Installation

1. Download Zip file under Releases
2. Unzip file
3. Run "CrossyChicken.exe"

## 🎯 How to Play

1. Select game mode from the main menu (Easy/Hard)
2. Use arrow keys or WASD to move your chicken
3. Traverse different terrains while avoiding obstacles:
   - Roads: Avoid cars
   - Rivers: Jump on logs to cross (you'll drown in water!)
   - Railways: Watch out for fast-moving trains
4. Each forward step increases your score
5. Reach the target score to complete the level


## 💡 Development Notes

### Architecture

The game is structured around these key components:

- **PlayerMovement**: Handles chicken movement, collision detection, and game state
- **WorldSpawner**: Manages procedural terrain generation and obstacle spawning
- **CarMovement/TrainMovement/LogMovement**: Control obstacle behaviors
- **DifficultyManager**: Singleton for managing game difficulty scaling
- **Controller**: Handles scene transitions between game modes

### Known Issues

- Log movement can sometimes be erratic at higher difficulty levels

## 📝 Future Enhancements

- [ ] Character selection with different chicken types
- [ ] Additional environmental hazards (predators, weather effects)
- [ ] Power-ups and special abilities
- [ ] Endless mode with increasing difficulty
- [ ] Global leaderboard system
- [ ] Mobile touch controls support
- [ ] Additional collectibles and achievements

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👏 Acknowledgments

- Inspired by the popular Crossy Road game
- Thanks to the Unity community for resources and support
- Sound effects from [PixaBaySFX](https://pixabaySFX.com)
- Models from [SketchFAB](https://sketchfab.com)
