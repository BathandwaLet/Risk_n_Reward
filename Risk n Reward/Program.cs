using System.Diagnostics;

namespace Risk_n_Reward;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Risk n' Reward!");
        Console.Clear();
        
        Console.WriteLine("What game do you want to play?");
        Console.WriteLine("1.Coin Toss \n 2.Black Jack \n 3.Crash \n 4.Pick Five \n 5.Rock, Paper, Scissors");
        int.TryParse(Console.ReadLine(), out var choiceNumber);
        string game;

        switch (choiceNumber)
        {
            case 1:
                game = "CoinToss";
                break;
            case 2:
                game = "BlackJack";
                break;
            case 3:
                game = "CrashSimulator";
                break;
            case 4:
                game = "PickFive";
                break;
            case 5:
                game = "RockPaperScissors";
                break;
            default: break;
        }
        
        
    }
}

