using Risk_n_Reward.Core.Wallet;
using Risk_n_Reward.Core.Engines;
using Risk_n_Reward.Core.Models.LuckyDiceModels;
using Risk_n_Reward.Core.Models.LuckyDiceModels.Results;
using Risk_n_Reward.Core.Results;
using static Risk_n_Reward.Games.LuckyDice.DiceRoll;

namespace Risk_n_Reward.Games.LuckyDice;

public class LuckyDice : IGame
{
    public void Start(WalletService wallet)
    {
        Console.Clear();
        
        Console.WriteLine("Welcome to Lucky Dice");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        
        const decimal minBetAmount = 10.0m;
        decimal playerBet = TryPlaceBet(minBetAmount, wallet);
        
        Console.Clear();
        
        Console.WriteLine("Rolling the Dice...");

        int [] diceRoll = DiceRoll.Roll().ToArray();
        
        
        Console.WriteLine($"\nYou threw \nDice 1: {diceRoll[0]} \nDice 2: {diceRoll[1]}");

        var engine = new LuckyDiceEngine();
        LuckyDiceResult result = engine.Result(diceRoll);

        if (result.IsWin)
        {
            wallet.Payout(playerBet * result.PayoutMultiplier);
            Console.WriteLine($"\nCONGRATULATIONS!");
            Console.WriteLine($"You won {playerBet * result.PayoutMultiplier} VMali!");
        }
        else
        {
            Console.WriteLine("\nNo win this time.");
        }

        Console.WriteLine($"Your new balance is: {wallet.Balance} VMali");
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