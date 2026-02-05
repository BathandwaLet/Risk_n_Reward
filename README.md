RISK N REWARD

PROJECT TITLE <br>
Risk n Reward <br>
A C# console-based application exploring probability-based games, risk and reward mechanics 

PROJECT DESCRIPTION <br>
Risk n Reward is a game logic written in C#. <br>
Its scope focuses on probability-based decision-making, betting mechanisms and reward mechanics/balancing. <br>
The current implementation is a console-based application intended to be expanded into a full platform. 


FEATURES
1) Risk vs reward game mechanics. <br>
2) Randomised outcomes. <br>
3) Modular game logic. <br>
4) Console-based interface. <br>
5) Centralised betting and payout logic. <br>
6) Balance and wager management.
7) Multiple mini games ()

PLANNED FEATURES (Updated)
1) Baccarat mini-game <br>
2) Simplified Texas Hold'em Poker mini game. <br>
3) Win streak system with bonus multipliers. <br>
4) Blazor WebAssembly frontend UI. <br>
5) Improved game ochrestration. 

TECH STACK <br>
1) Programming language: C# <br>
2) Framework: .NET 9.0 <br>
3) Frontend (Planned): Blazor WebAssembly (WASM) <br>
4) Version control: Git

HOW TO RUN <br>
1) Clone Repository <br>
2) Open on any IDE that supports C# with .NET 9.0, e.g Visual Studio, Rider, etc. <br>
3) Build the solution <br>
4) Run the application in console (browser support coming soon) 

PROJECT STRUCTURE<br>
1) Games: Deals with game logic and game ochrestration <br>  
    • Initialises game engine. <br>
    • Updates player wallet. <br>
2. Core (Game Mechanics and Utilities)<br>
    i - Engine<br>
    Responsible for game-specific logic, including:<br>
    • Rule enforcement<br>
    • Outcome calculation<br>
    • Probability handling<br>
    • Payout determination<br>
    Each casino game has its own engine implementation.<br>

    ii - Models<br>
    Defines the data structures used across the system, such as:<br>
    • Bet types<br>
    • Outcomes and results<br>
    • Game states<br>
    • Player actions<br>

4. Interfaces<br>
Acts as the contract between games and the system, ensuring:<br>
• Consistent interaction between engines<br>
• Interchangeable game implementations<br>
• Decoupling of logic from UI<br>

5. User Interface<br>
Console-based UI for testing and development<br>
Blazor WebAssembly UI (planned) for interactive web gameplay<br>

6. Wallet and WalletService<br>
i - Wallet<br>
• Represents the player’s balance and transaction history.<br>

ii - WalletService<br>
Handles:<br>
Deposits and withdrawals<br>
Bet deductions<br>
Winnings and payouts<br>
Validation of sufficient funds<br>

HIGH LEVEL ARCHITECTURE DIAGRAM<br>
<img width="121" height="881" alt="RnRCompleteHighLevelArch drawio" src="https://github.com/user-attachments/assets/97beedf5-886d-463f-b6b3-d4367005ddb2" />


ROADMAP<br>
• Complete Baccarat and Texas Hold'em mini-games. <br>
• Complete Winstreak bonus system. <br>
• Add unit and probalbility tests. <br>
• Implement Blazor WebAssembly frontend. <br>
• Prepare for deployment.

LICENSE<br>
This project is licensed under the MIT License. <br>
See the LICENSE file for further information

DISCLAIMER
Risk n Reward is a virtual arcade simulation, a personal programming project created for educational and portfolio purposes. It utilises a virtual currency, VMali, and there is no intent to switch to actual currency or other items of value that can be deposited, wagered or won. This project is solely to demonstrate technical skills and as practice in software development. This is not intended to promote, facilitate or simulate gambling. Use of the simulation is for educational and experimental purposes only.

AUTHOR <br>
Bathandwa L Maphumulo <br>
Email: bmap750@gmail.com <br>
LinkedIn: in/bathandwa-maphumulo-216177180
