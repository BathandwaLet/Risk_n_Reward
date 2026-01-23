using Risk_n_Reward.Games.Baccarat;
using Risk_n_Reward.Games.HighLow;
using Risk_n_Reward.Games.Roulette;
using Risk_n_Reward.Games.TexasHoldemPoker;
using Risk_n_Reward.Games.Slots;
using Risk_n_Reward.Games.LuckyDice;



namespace Risk_n_Reward;

using System.Diagnostics;
using Risk_n_Reward.Games.BlackJack;
using Risk_n_Reward.Games.Crash;
using Risk_n_Reward.Games.CoinToss;
using Risk_n_Reward.Games.RockPaperScissors;
using Risk_n_Reward.Games.PickFive;
using Risk_n_Reward.Games.LuckyDice;
using Risk_n_Reward.Wallet;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Risk n' Reward!");
        Console.Clear();
        
        var walletService = new WalletService();

        while (true)
        {
            Console.WriteLine("What game do you want to play? \n Enter the number corresponding with the game");
            Console.WriteLine(" 1.Coin Toss \n 2.Black Jack \n 3.Crash \n 4.Pick Five \n 5.Rock, Paper, Scissors" + 
                              "\n 6.Lucky Dice \n 7.High Low \n 0.Exit");
        
            int.TryParse(Console.ReadLine(), out var choiceNumber);
            IGame? game = null;
        
            switch (choiceNumber)
            {
                case 1:
                    game = new CoinToss();
                    break;
                case 2:
                    game = new BlackJack();
                    break;
                case 3:
                    game = new Crash();
                    break;
                case 4:
                    game = new PickFive();
                    break;
                case 5:
                    game = new RockPaperScissors();
                    break;
                case 6:
                    game = new LuckyDice();
                    break;
                case 7:
                    game = new HighLow();
                    break;
                case 8:
                    game = new Roulette();
                    break;
                case 9:
                    game = new Baccarat();
                    break;
                case 10:
                    game = new Slots();
                    break;
                case 11:
                    game = new TexasHoldem();
                    break;
                case 0:
                    Console.WriteLine("Thank you for visiting Risk n Reward!");
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        
            game.Start(walletService);
            
            while (PlayAgain())
            {
                game.Start(walletService);
            }
            
            
        }
        
        
    }

    private static bool PlayAgain()
    {
        Console.WriteLine("Would you like to play again? Y/N");

        while (true)
        {
            var key = Console.ReadKey(true).Key;
        
            if (key == ConsoleKey.Y) return true;
            if (ConsoleKey.N == key) return false;

            Console.WriteLine("Invalid input!");
        }

    }
}

