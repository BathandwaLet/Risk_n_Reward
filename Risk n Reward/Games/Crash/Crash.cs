using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Engines.CrashEngine;
using Risk_n_Reward.Core.Models.CrashModels.GameOutcomes;

namespace Risk_n_Reward.Games.Crash;

public class Crash : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Crash!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");

        Console.Clear();
        
        const decimal minBetAmount = 0.01m;
        decimal playerBet = TryPlaceBet(minBetAmount, wallet);
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        Console.WriteLine("\n Press C to cash out \n");
        
        var engine = new CrashEngine();
        var result = engine.Result();
        
        var multiplier = result.PayoutMultiplier;
        var crashPointMultiplier = result.CrashPointMultiplier;
        var outcome = result.Outcome;

        if (result.Outcome == CrashOutcomes.Win)
        {
            Console.Clear();
            Console.WriteLine($"\n CONGRATULATIONS\n you cashed out at {multiplier}x and " +
                              $"earned {playerBet*multiplier} VMali.");
            wallet.Payout(playerBet * multiplier);
        }

        if (result.Outcome == CrashOutcomes.Lose)
        {
            Console.Clear();
            Console.WriteLine($"\nCRASH!\n at {crashPointMultiplier}x");
        }
        
        Console.WriteLine($"Your new balance is {wallet.Balance} VMali.");
        
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