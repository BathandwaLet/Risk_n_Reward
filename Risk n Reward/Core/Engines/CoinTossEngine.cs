using Risk_n_Reward.Core.Results;
using Risk_n_Reward.Games.CoinToss;
using Risk_n_Reward.Core.Models;

namespace Risk_n_Reward.Core.Engines;

public class CoinTossEngine
{
    public CoinTossResult Result(CoinSide player, CoinSide computer)
    {
        bool result = GameResult(player, computer);
        decimal payoutMultiplier = Payout(player, computer);
        
        return new CoinTossResult 
        {
            win = result,
            Payout = payoutMultiplier,
        };
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
}