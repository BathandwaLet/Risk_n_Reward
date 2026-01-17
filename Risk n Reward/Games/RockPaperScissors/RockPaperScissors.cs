using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Engines;
using Risk_n_Reward.Core.Models.RockPaperScissorsModels;
using Risk_n_Reward.Core.Models.RockPaperScissorsModels.Results;
using static Risk_n_Reward.Games.RockPaperScissors.ComputerRPS;

namespace Risk_n_Reward.Games.RockPaperScissors;

public class RockPaperScissors : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Rock, Paper , Scissors!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");

        Console.WriteLine("Place your bet:");
        decimal playerBet;
        if (!decimal.TryParse(Console.ReadLine(), out playerBet))
        {
            throw new ArgumentException("Invalid input!");
        }

        if (!wallet.PlaceBet(playerBet))
        {
            throw new ArgumentException("Insufficient funds!");
        }
        
        Console.WriteLine($"Press R for rock, P for Paper and S for Scissors.");
        RPS playerChoice = ParsePlayerChoice(Console.ReadLine().ToUpper());

        if (playerChoice == RPS.Null)
        {
            throw new ArgumentException("Error! \n Incorrect selection!");
        }
        
        var computer = new ComputerRPS();
        RPS computerChoice = computer.ComputerPick();

        var engine = new RockPaperScissorsEngine();
        RockPaperScissorsResult result = engine.Result(playerChoice, computerChoice);

        Console.WriteLine($"You choose {playerChoice} \nThe computer choose {computerChoice}");

        if (result.IsWin && result.PayoutMultiplier == 2.0m) 
        {
            Console.WriteLine($"\nCONGRATULATIONS\nYou have won{playerBet * result.PayoutMultiplier} VMali!");
            wallet.Payout(playerBet * result.PayoutMultiplier);
        }
        else if (result.IsWin && result.PayoutMultiplier == 1.0m)
        {
            Console.WriteLine($"\nDraw\nYou bet amount of {playerBet} VMali has been refunded");
            wallet.Payout(playerBet);
        }
        else
        {
            Console.WriteLine("No win this time!");
        }

        Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        Console.ReadKey();
    }

    private static RPS ParsePlayerChoice(string input)
    {
        return input switch
        {
            "R" => RPS.Rock,
            "P" => RPS.Paper,
            "S" => RPS.Scissors,
            _ => RPS.Null
        };
    }
}