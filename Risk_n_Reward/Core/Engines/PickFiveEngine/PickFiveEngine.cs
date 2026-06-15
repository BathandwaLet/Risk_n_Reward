using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.PickFiveModels.Results;
using Risk_n_Reward.Core.Results;
using Risk_n_Reward.Games.PickFive;
using static Risk_n_Reward.Core.Models.PickFiveModels.Results.PickFiveResult;

namespace Risk_n_Reward.Core.Engines;

public class PickFiveEngine
{
    public PickFiveResult Result(int [] player, int [] computer)
    {
        int matches = Matches(player, computer);
        bool bonusBall = BonusBallMatch(player, computer);
        decimal payout = GamePayout(matches, bonusBall);
        
        return new PickFiveResult 
        {
            BallMatches = matches,
            BonusBallMatch = bonusBall,
            Payout = payout
        };
    }
    
    private int Matches (int [] player, int [] computer)
    {
        int matches = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (player[i] == computer[j])
                {
                    matches++;
                }
            }
        }

        return matches;
    }

    private bool BonusBallMatch(int [] player, int [] computer)
    {
        if (player[5] == computer[5])
        {
            return true;
        }

        return false;
    }

    private decimal GamePayout(int matches, bool bonusBall)
    {
        return (matches, bonusBall) switch
        {
            (0, true) => 1000m,
            (3, false) => 15000m,
            (3, true) => 25000m,
            (4, false) => 50000m,
            (4, true) => 75000m,
            (5, false) => 75000m,
            (5, true) => 150000m,
            _ => 0m
        };
    }
    
    
}