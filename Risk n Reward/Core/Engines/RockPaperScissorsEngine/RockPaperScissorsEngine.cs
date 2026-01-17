using Risk_n_Reward.Core.Models.RockPaperScissorsModels;
using Risk_n_Reward.Core.Models.RockPaperScissorsModels.Results;
using Risk_n_Reward.Core.Results;
using  Risk_n_Reward.Games.RockPaperScissors;
using static Risk_n_Reward.Games.RockPaperScissors.ComputerRPS;

namespace Risk_n_Reward.Core.Engines;

public class RockPaperScissorsEngine
{
    public RockPaperScissorsResult Result(RPS player, RPS computer)
    {
        int result = GameResult(player, computer);
        decimal payout = Payout(result);

        return new RockPaperScissorsResult()
        {
            Result = result,
            PayoutMultiplier = payout,
        };
    }
    
    private int GameResult(RPS playerDraw, RPS computerDraw)
    {
        if (playerDraw == computerDraw)
        {
            return 1;
        }

        bool isWin = ((playerDraw == RPS.Rock && computerDraw == RPS.Scissors) ||
                      (playerDraw == RPS.Paper && computerDraw == RPS.Rock) ||
                      (playerDraw == RPS.Scissors && computerDraw == RPS.Paper));

        if (isWin)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    private decimal Payout(int gameResult)
    {
        if (gameResult == 0)
        {
            return 0.0m;
        }
        else if (gameResult == 1)
        {
            return 1.0m;
        }
        else
        {
            return 2.0m;
        }
    }
}