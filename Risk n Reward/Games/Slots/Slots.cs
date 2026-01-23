using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Engines.SlotsEngine;
using Risk_n_Reward.Core.Models.SlotsModel.Results;
namespace Risk_n_Reward.Games.Slots;

public class Slots : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Slots!");

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
        
        var engine = new SlotsEngine();
        SlotsResult result = engine.Result();
        
        Console.WriteLine("Spinning...");
        Thread.Sleep(1000);

        foreach (var reel in result.ReelsOutcome)
        {
            Console.Write($"{reel} ");
            Thread.Sleep(500);
        }
        
        Console.WriteLine();
        
        if (result.IsWin)
        {
            Console.WriteLine($"CONGRATULATIONS!\nYou won {playerBet * result.PayoutMultiplier}VMali!");
            wallet.Payout(playerBet * result.PayoutMultiplier);
        }
        else
        {
            Console.WriteLine("No win this time");
        }
        
        Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        Console.ReadKey();

    }
}