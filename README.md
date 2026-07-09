# RISK N REWARD  

## PROJECT TITLE  
**Risk n Reward**  
 
An ASP.NET Core MVC application that was prototyped as a console-based application. Risk n Reward  explores probability-based games, risk and reward mechanics. 

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
- Web-based UI.
- Centralised betting and payout logic.
- Balance and wager management.
- Multiple casino-style mini games.

### Deprecated
- Rock, Paper, Scissors (removed as it no longer aligns with the casino focus of the project)

## PLANNED FEATURES (Updated)
- Web-based UI of the games.
- Win streak system with bonus multipliers.

## TECH STACK
- Programming Language(s): **C#**.
- Framework(s): **.NET 10.0**.
- Database (Planned): **PostgreSQL**.
- Authentication (Planned): **ASP.NET Core Identity**
- Frontend (Planned): **Razor pages(ASP.NET Core MVC)**
- Version Control: **Git**

## HOW TO RUN  
- Clone the repository.  
- Open in any IDE that supports C# with .NET 10.0 (Visual Studio, Rider, etc).
- Build the solution.
- Run the application in your browser.

## PROJECT STRUCTURE 
```
Risk_n_Reward/
├── .github/
│   └── ISSUE_TEMPLATE/
│       ├── bug_report.md
│       └── feature_request.md
├── .gitignore
├── CODE_OF_CONDUCT.md
├── CONTRIBUTING.md
├── global.json
├── LICENSE
├── README.md
├── Risk_n_Reward/ #Console UI
│   ├── Games/
│   │   ├── Baccarat/
│   │   ├── BlackJack/
│   │   ├── CoinToss/
│   │   ├── Crash/
│   │   ├── HighLow/
│   │   ├── LuckyDice/
│   │   ├── PickFive/
│   │   ├── Roulette/
│   │   ├── Slots/
│   │   └── TexasHoldemPoker/
│   ├── Program.cs
│   └── Risk_n_Reward.csproj
├── Risk_n_Reward.Core/ #Class Library contains game engines, models, interfaces, winstreak, wallet.
│   ├── Core/
│   │   ├── Engines/
│   │   │   ├── BaccaratEngine/
│   │   │   ├── BlackJackEngine/
│   │   │   ├── CoinTossEngine/
│   │   │   ├── CrashEngine/
│   │   │   ├── HighLowEngine/
│   │   │   ├── LuckyDiceEngine/
│   │   │   ├── PickFiveEngine/
│   │   │   ├── RouletteEngine/
│   │   │   ├── SlotsEngine/
│   │   │   └── TexasHoldemEngine/
│   │   ├── Models/
│   │   │   ├── BaccaratModels/
│   │   │   │   ├── BetTypes/
│   │   │   │   ├── Outcomes/
│   │   │   │   └── Results/
│   │   │   ├── BlackJackModels/
│   │   │   │   ├── BetOptions/
│   │   │   │   ├── Outcomes/
│   │   │   │   └── Results/
│   │   │   ├── CardDeck/
│   │   │   ├── CoinTossModels/
│   │   │   │   ├── CoinSide/
│   │   │   │   ├── Outcomes/
│   │   │   │   └── Results/
│   │   │   ├── CrashModels/
│   │   │   │   ├── GameOutcomes/
│   │   │   │   └── Results/
│   │   │   ├── HighLowModels/
│   │   │   │   ├── BetTypes/
│   │   │   │   ├── Outcomes/
│   │   │   │   └── Results/
│   │   │   ├── LuckyDiceModels/
│   │   │   │   └── Results/
│   │   │   ├── PickFiveModels/
│   │   │   │   └── Results/
│   │   │   ├── RouletteModels/
│   │   │   │   ├── BetTypes/
│   │   │   │   ├── Outcomes/
│   │   │   │   └── Results/
│   │   │   ├── SlotsModel/
│   │   │   │   ├── Outcomes/
│   │   │   │   ├── Results/
│   │   │   │   └── Symbols/
│   │   │   └── TexasHoldemModels/
│   │   │       ├── Outcomes/
│   │   │       │   ├── GameResult/
│   │   │       │   └── HandType/
│   │   │       └── Results/
│   │   └── Results/
│   ├── Interfaces/
│   ├── Risk_n_Reward.Core.csproj
│   ├── Wallet/
│   └── WinStreak/
├── Risk_n_Reward.sln
├── Risk_n_Reward.Web/ #Web Implementation.
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── Controllers/
│   ├── Data/
│   ├── Hub/
│   ├── Migrations/
│   ├── Models/
│   ├── Program.cs
│   ├── Properties/
│   ├── Risk_n_Reward.Web.csproj
│   ├── Views/
│   │   ├── CoinToss/
│   │   ├── Crash/
│   │   ├── Home/
│   │   ├── PickFive/
│   │   ├── Settings/
│   │   ├── Shared/
│   │   └── Slots/
│   └── wwwroot/
│       ├── css/
│       ├── images/
│       │   ├── contact-icons/
│       │   ├── favicon/
│       │   ├── gameassets/
│       │   │   ├── cointoss/
│       │   │   ├── deck-of-cards/
│       │   │   └── slots/
│       │   ├── home-icons/
│       │   └── site-icons/
│       ├── js/
│       │   └── site.js
│       └── lib/
│           ├── bootstrap/
│           │   ├── dist/
│           │   │   ├── css/
│           │   │   └── js/
│           │   └── LICENSE
│           ├── jquery/
│           │   └── dist/
│           ├── jquery-validation/
│           │   ├── dist/
│           │   └── LICENSE.md
│           └── jquery-validation-unobtrusive/
│               └── dist/
└── SECURITY.md
```
 

## HIGH LEVEL ARCHITECTURE DIAGRAM
![Risk n Reward High Level Architecture](https://github.com/user-attachments/assets/97beedf5-886d-463f-b6b3-d4367005ddb2)

## ROADMAP  
### PHASE 1: Project Scaffold
- [X] Added ASP.NET Core MVC project to Risk_n_Reward solution file.
- [X] Set up EF Core.
- [X] Scaffold database tables.

### PHASE 2: Game by Game Migration
- [ ] Baccarat.
- [ ] BlackJack.
- [X] Coin Toss.
- [ ] Crash.
- [ ] High Low.
- [ ] Lucky Dice.
- [ ] Pick Five.
- [ ] Roulette.
- [X] Slots.
- [ ] Texas Hold'em Poker.

### PHASE 3: Implementation of GameSessions, WinStreaks, Leaderboard
- [ ] Game sessions.
- [ ] Win streaks.
- [ ] Leaderboard for each game.

### PHASE 4: Auth + Wallet
- [ ] Implement ASP.NET Identity.
- [ ] Wire WalletService so new players start with 1000 VMali

### PHASE 5: Deployment
- [ ] Deploy to Railway/Render with a managed PostgreSQL instance.


## LICENSE
This project is licensed under the MIT License.  
See the LICENSE file for further information.  

## DISCLAIMER
Risk n Reward is a virtual arcade simulation, a personal programming project created for educational and portfolio purposes. It utilises a virtual currency, VMali, and there is no intent to switch to actual currency or other items of value that can be deposited, wagered or won. This project is solely to demonstrate technical skills and as practice in software development. This is not intended to promote, facilitate or simulate gambling. Use of the simulation is for educational and experimental purposes only.  

## AUTHOR
**Bathandwa L Maphumulo**  
Email: bmap750@gmail.com  
LinkedIn: [in/bathandwa-maphumulo-216177180](https://www.linkedin.com/in/bathandwa-maphumulo-216177180/)
