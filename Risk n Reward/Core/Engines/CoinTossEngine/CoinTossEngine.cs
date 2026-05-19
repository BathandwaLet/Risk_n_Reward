using Risk_n_Reward.Core.Results;
using Risk_n_Reward.Games.CoinToss;
using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.CoinTossModels.Outcomes;
using Risk_n_Reward.Core.Models.CoinTossModels.Results;

namespace Risk_n_Reward.Core.Engines;

public class CoinTossEngine
{
    public CoinTossResult Result(CoinSide player, CoinSide computer)
    {
        bool result = PlayGame(player, computer);
        decimal payoutMultiplier = Payout(result);
        
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
                playerChoice = CoinSide.Heads;
            }
            else if (key == ConsoleKey.T)
            {
                playerChoice = CoinSide.Tails;
            }
            
            computerChoice =  ComputerSelection();
        }

        bool result = (GameResult(playerChoice, computerChoice) == CoinTossOutcomes.Win )? true: false;
        
        return result;
    }
    
    private CoinTossOutcomes GameResult(CoinSide player, CoinSide computer)
    {
        if (player == computer)
        {
            return CoinTossOutcomes.Win;
        }
        else
        {
            return CoinTossOutcomes.Lose;
        }
    }

    private decimal Payout(bool gameResult)
    {
        if (gameResult)
        {
            return 1.5m;
        }

        return 0m;
    }
    
    public static CoinSide ComputerSelection()
    {
        Random rnd = new Random();
        CoinSide  computerChoice =  (rnd.Next(0, 2) == 0)? CoinSide.Heads : CoinSide.Tails;

        return computerChoice;
    }
}