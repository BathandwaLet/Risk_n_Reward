# RISK N REWARD  

## PROJECT TITLE  
**Risk n Reward**  

A C# console-based application exploring probability-based games, risk and reward mechanics.  

## PROJECT DESCRIPTION  
Risk n Reward is a virtual arcade/casino with its games and game logic written in C#.  

Its scope focuses on:  
- Probability-based decision-making.  
- Betting mechanisms.
- Reward mechanics and balancing.

The current implementation is a console-based application and will be expanded into a full platform.  

## FEATURES
- Risk vs reward game mechanics.  
- Randomised but fair outcomes.
- Modular game logic.
- Console-based UI.
- Centralised betting and payout logic.
- Balance and wager management.
- Multiple casino-style mini games.

### Deprecated
- Rock, Paper, Scissors (removed as it no longer aligns with the casino focus of the project)

## PLANNED FEATURES (Updated)
- Refactor of Crash mini game into separate modules.
- Win streak system with bonus multipliers.
- Game persistence.  
- Database integration.
- Blazor WebAssembly frontend UI
- Improved game ochrestration and performance.

## TECH STACK
- Programming Language(s): **C#**.
- Framework(s): **.NET 10.0**.
- Database (Planned): **PostgreSQL**.
- Authentication (Planned): **ASP.NET Core Identity**
- Frontend (Planned): **Blazor WebAssembly (WASM)**
- Version Control: **Git**

## HOW TO RUN  
- Clone the repository.  
- Open in any IDE that supports C# with .NET 10.0 (Visual Studio, Rider, etc).
- Build the solution.
- Run the application in the console window.

## PROJECT STRUCTURE  

### 1. Games-Game Logic and Ochrestration  
- Console UI
- Manages gameplay and game flow.
- Initialises game engine.  
- Updates player wallet.

### 2. Core - Game Mechanics and Utilities  

#### Engine
Responsible for game-specific logic, including:
- Rule enforcement.  
- Outcome calculation.  
- Probability handling.  
- Payout (multiplier) determination.

Each mini-game has its own game engine implementation.  

#### Models
Defines shared data structures used across the system, such as:
- Bet types.  
- Outcomes and results.  
- Game states.  
- Player actions.  

### 3. Interfaces
Acts as the contract between games and the system, ensuring:
- Consistent interaction between games, game engines and models.
- Interchangeable game implementations.
- Decoupling of logic from UI.  

### 4. User Interface
- Blazor WebAssembly UI (planned) for interactive web gameplay.  

### 5. Wallet and WalletService  

#### Wallet
Represents the player's balance.

#### WalletService  
Handles:  
- Deposits and withdrawals into the user's wallet.
- Bet deductions.
- Winnings and Payouts.
- Validation of sufficient funds to play.  

## HIGH LEVEL ARCHITECTURE DIAGRAM
![Risk n Reward High Level Architecture](https://github.com/user-attachments/assets/97beedf5-886d-463f-b6b3-d4367005ddb2)

## ROADMAP  
- Refactor Crash mini-game into modular components.  
- Add unit and probability tests.  
- Implement WinStreak bonus multiplier system.  
- Implement basic Blazor WebAssembly frontend.
- Implement game persistence and a database.  
- Implement user login and authentication.
- Improve frontend.
- Implement a History log, player wins, losses and favourite game.
- Prepare for deployment.  

## LICENSE
This project is licensed under the MIT License.  
See the LICENSE file for further information.  

## DISCLAIMER
Risk n Reward is a virtual arcade simulation, a personal programming project created for educational and portfolio purposes. It utilises a virtual currency, VMali, and there is no intent to switch to actual currency or other items of value that can be deposited, wagered or won. This project is solely to demonstrate technical skills and as practice in software development. This is not intended to promote, facilitate or simulate gambling. Use of the simulation is for educational and experimental purposes only.  

## AUTHOR
**Bathandwa L Maphumulo**  
Email: bmap750@gmail.com  
LinkedIn: [in/bathandwa-maphumulo-216177180](https://www.linkedin.com/in/bathandwa-maphumulo-216177180/)
