using Risk_n_Reward.Core.Results;
using Risk_n_Reward.Games.CoinToss;
using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.CoinTossModels.Results;

namespace Risk_n_Reward.Core.Engines;

public class CoinTossEngine
{
    public CoinTossResult Result(CoinSide player, CoinSide computer)
    {
        bool result = PlayGame(player, computer);
        decimal payoutMultiplier = Payout(player, computer);
        
        return new CoinTossResult 
        {
            Win = result,
            Payout = payoutMultiplier,
        };
    }

    public bool PlayGame(CoinSide playerChoice, CoinSide computerChoice)
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.H)
            {
                playerChoice = CoinSide.H;
            }
            else if (key == ConsoleKey.T)
            {
                playerChoice = CoinSide.T;
            }
            
            computerChoice =  ComputerSelection();
        }

        var result = GameResult(playerChoice, computerChoice);
        
        return result;
    }
    
    private bool GameResult(CoinSide player, CoinSide computer)
    {
        if (player == computer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private decimal Payout(CoinSide player, CoinSide computer)
    {
        if (GameResult(player, computer))
        {
            return 1.5m;
        }

        return 0m;
    }
    
    public static CoinSide ComputerSelection()
    {
        Random rnd = new Random();
        CoinSide  computerChoice =  (rnd.Next(0, 2) == 0)? CoinSide.H : CoinSide.T;

        return computerChoice;
    }
}