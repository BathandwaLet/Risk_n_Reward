using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.CrashModels.GameOutcomes;
//using Risk_n_Reward.Core.Games.Crash;

namespace Risk_n_Reward.Core.Engines.CrashEngine;

public class CrashEngine
{
    public CrashResult Result()
    {
        decimal crashPoint = CrashPoint();
        decimal multiplier = PlayGame(crashPoint);
        decimal payoutMultiplier = PayoutMultiplier(multiplier);
        var outcome = EvaluateGame(payoutMultiplier);
        
        return new CrashResult()
        {
            Outcome = outcome,
            CrashPointMultiplier = crashPoint,
            PayoutMultiplier = payoutMultiplier,
        };
    }

    public decimal PlayGame(decimal crashPoint)
    {
        decimal multiplier = 1m;
        
        while (multiplier < crashPoint)
        {
            multiplier *= 1.01m;
            multiplier = Math.Round(multiplier, 2);
            
            if (multiplier >= crashPoint)
            {
                return -1;
                break;
            }
            
            Thread.Sleep(200);
            Console.WriteLine(multiplier);
            
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.C)
                {
                    if (multiplier < crashPoint)
                    {
                        return multiplier;
                    }

                    break;
                }
            }
        }

        return multiplier;

    }
    
    private decimal CrashPoint()
    {
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

    private CrashOutcomes EvaluateGame(decimal payout)
    {
        if (payout != 0)
        {
            return CrashOutcomes.Win;
        }

        return CrashOutcomes.Lose;
    }

    private decimal PayoutMultiplier(decimal multiplier)
    {
        return (multiplier) switch
        {
            -1 => 0,
            _ => multiplier,
        };
    }
    
    private static readonly Random rnd = new Random();
}