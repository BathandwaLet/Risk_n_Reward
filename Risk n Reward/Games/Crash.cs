using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games;

public class Crash : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Crash!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");

        Console.WriteLine("Place your bet:");
        decimal.TryParse(Console.ReadLine(), out decimal playerBet);

        if (!wallet.PlaceBet(playerBet))
        {
            Console.WriteLine("Insufficient Funds!");
            return;
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        Console.WriteLine("Press C to cash out");
        string cashout =  "";

        decimal multiplier = 1.00m;
        
        
        

    }

    public static decimal CrashPoint()
    {
        Random rnd = new Random();
        
        const decimal houseEdge = 0.05m;
        
        var r = rnd.NextDouble();

        var crashPoint = 0m;

        while (crashPoint < 1)
        {
            crashPoint = (1 - houseEdge) / (decimal)r;

            if (crashPoint > 1m)
            {
                return crashPoint;
            }
        }

        return 0;
    }
}