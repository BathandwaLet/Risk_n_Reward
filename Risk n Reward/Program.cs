namespace Risk_n_Reward;

using System.Diagnostics;
using Risk_n_Reward.Games;
using Risk_n_Reward.Wallet;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Risk n' Reward!");
        Console.Clear();
        
        Console.WriteLine("What game do you want to play? \n Enter the number corresponding with the game");
        Console.WriteLine("1.Coin Toss \n 2.Black Jack \n 3.Crash \n 4.Pick Five \n 5.Rock, Paper, Scissors");
        
        int.TryParse(Console.ReadLine(), out var choiceNumber);
        IGame? game = null;
        
        var walletService = new WalletService();
        
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
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        
        game.Start(walletService);
        
        
    }
}

