using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games.Crash;

public class Crash : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Crash!");

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
            return;
        }
        Console.Clear();
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        Console.WriteLine("\n Press C to cash out \n");
        bool cashout = false;

        decimal multiplier = 1.00m;

        var crashPoint = CrashPoint();

        while (multiplier < crashPoint)
        {
            multiplier *= 1.01m;
            multiplier = Math.Round(multiplier, 2);
            
            Thread.Sleep(200);
            Console.WriteLine(multiplier);
            
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.C)
                {
                    if (multiplier < crashPoint)
                    {
                        Console.Clear();
                        Console.WriteLine($"\n CONGRATULATIONS\n you cashed out at {multiplier}x and " +
                                          $"earned {playerBet*multiplier} VMali.");
                        wallet.Payout(playerBet * multiplier);
                        Console.WriteLine($"Your new balance is {wallet.Balance} VMali.");
                        cashout = true;
                    }

                    break;
                }
            }
        }

        if (cashout == false)
        {
            Console.Clear();
            Console.WriteLine($"\nCRASH!\n at {multiplier}x");
        }
        

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
                return Math.Round(crashPoint,2);
            }
        }

        return 0;
    }
}