using Risk_n_Reward.Wallet;
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
        Console.WriteLine("Welcome to Lucky Dice");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        Console.WriteLine("Place your bet");
        decimal playerBet;
        if (!decimal.TryParse(Console.ReadLine(), out playerBet))
        {
            throw new ArgumentException("Invalid input!");
        }

        if (!wallet.PlaceBet(playerBet))
        {
            throw new ArgumentException("Insufficient funds!");
            return;
        }
        
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
}