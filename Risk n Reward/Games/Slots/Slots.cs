using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Engines;
using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Results;
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
        
        //player choices
        
        var engine = new SlotsEngine();
        SlotsResult result = engine.Result();

    }
}