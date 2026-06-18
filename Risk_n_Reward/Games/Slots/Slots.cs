using Risk_n_Reward.Core.Wallet;
using Risk_n_Reward.Core.Engines.SlotsEngine;
using Risk_n_Reward.Core.Models.SlotsModel.Results;

namespace Risk_n_Reward.Games.Slots;

public class Slots : IGame
{
    public void Start(WalletService wallet)
    {
        Console.Clear();
        
        Console.WriteLine("Welcome to Slots!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");

        const decimal minBetAmount = 0.01m;
        decimal playerBet = TryPlaceBet(minBetAmount, wallet);
        
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
    
    public decimal TryPlaceBet(decimal minBetAmount,WalletService wallet)
    {
        decimal validBet;
        
        while (true)
        {
            Console.WriteLine("Place your bet: ");
            string betAmount = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(betAmount))
            {
                Console.WriteLine("You did not enter anything.");
                continue;
            }
            
            if (!decimal.TryParse(betAmount, out validBet))
            {
                Console.WriteLine("Please enter a value between 0 and 999999 as a bet amount.");
                continue;
            }
            
            if (validBet < minBetAmount)
            {
                Console.WriteLine($"Please enter an amount greater than {minBetAmount} VMali.");
                continue;
            }
            
            if (!wallet.PlaceBet(validBet))
            {
                Console.WriteLine($"Insufficient funds!");
                continue;
            }
            
            Console.WriteLine("Bet placed sucessfully!");
            return validBet;
        }
        
    }
    
}