# Orbital 2025 - Spicy Tartar Chicken Nanban Don

**Team ID:** 7366  
**Team Name:** Spicy Tartar Chicken Nanban Don  
**Proposed Level of Achievement:** Apollo 11


## Motivation

As both of us are avid fans of tower defence games, we wanted to try our hand at making one of our own. The motivation behind this tower defense game is to create a highly engaging and challenging gameplay experience for players.


## Aim

The game will feature both melee and ranged towers, each with unique abilities and upgrade paths, as well as diverse enemies with different movement and attack patterns.  

Interactions between towers and enemies will also help shape the flow of the combat. Such interactions include enemy blocking, tower synergies and terrain manipulation.

Our key goals for this game consist of:
- Creating a variety of tower and enemy types that interact in meaningful or unpredictable ways
- Introducing environmental elements that players (or enemies) can interact with
- Supporting multiple strategies or playstyles (no One Size Fits All approach), encouraging experimentation and stategising

Ultimately, our aim is to create a game that challenges players not just to react, but to adapt, plan, and think creatively under pressure.

## User Stories

- As a strategic player, I can deploy both melee and ranged towers with unique abilities so that I can experiment with different combinations and adapt to enemy types.
- As a player who values immersion, I can see detailed animations and visual effects for towers, enemies, and projectiles so that the gameplay feels more dynamic and engaging.
- As a challenge-seeking player, I can face waves of increasingly powerful enemies while managing limited resources, encouraging me to think critically and weigh the priorities of different choices.
- As a creative player, I can interact with the terrain/environment so that I can influence the battlefield and come up with unique strategies.


## Architecture


## Workflow
[insert game loop] 

## Features

### Core Features
1. **Map layout**
2. **Enemy pathing**
3. **Enemy and tower classes**
4. **Projectile behaviour and interaction with enemies**
5. **User Interface (UI)**

### Extension Features
6. **Levelling system**
7. **Stage obstacles**
8. **Enemy - Tower interaction** (enemy can attack towers)
9. **Improving sprites and graphics**


## Timeline

### Liftoff (12 May - 19 May)
- Designing liftoff poster and presentation video
- Learn how to effectively version control using Git/Github through workshops

### Milestone 1 - Technical proof of concept
- Features 1, 2 base completed (Pathing)
- Feature 3 worked on (basic enemy and towers for testing)
- Feature 4 started on (using what we have from feature 3)

### Milestone 2 - Prototype
- Feature 4 base completed (more projectiles can be added over time)
- Multiple unique classes defined under Feature 3
- Feature 5 completed (basic UI to include functions like pause, restart etc)
- Attempt to add Feature 7 (Obstacles that can block path, damage enemies etc)

### Milestone 3 - Extended system
- Try implementing Feature 8 if possible (by making use of ideas from Feature 7)
- Complete Feature 9
- Implement Feature 6 if possible


## Tech Stack

Our game will be developed using the following tools and technologies:

- **Godot Engine**  
  A lightweight, open-source game engine used to build our 2D gameplay systems and visuals.

- **GDScript**  
  Godot’s built-in scripting language, ideal for rapid prototyping and implementing core game logic.

- **C#**  

- **Piskel**  
  A free online pixel art editor used to create and animate tower, enemy, and environment sprites.

- **Git & GitHub**  
  Used for version control, collaboration, issue tracking, and managing the development process.


## Software Engineering

To achieve our goal of Apollo 11, we plan to implement the following software engineering practices:

- **Encapsulation**: We will use classes to represent different entities, such as enemies, towers or stage obstacles.
- **Inheritance & Polymorphism**: We will create layers of abstraction using inheritance and use polymorphism to define unique methods of attack/movement for each entity.  
- **Composition**: We will use separate classes and interfaces to define base characteristics such as area of effect, upgrades etc and include them in entity classes.  
- **Git & GitHub**: We will use Git for version control to manage code and collaborate efficiently, and GitHub will serve as the central repository.  
- **Code Documentation**: We will write clear, consistent documentation for all code.

## Documentation
