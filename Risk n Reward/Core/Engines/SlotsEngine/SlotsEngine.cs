using Risk_n_Reward.Core.Models.SlotsModel.Outcomes;
using Risk_n_Reward.Core.Models.SlotsModel;
using Risk_n_Reward.Core.Models.SlotsModel.Results;
using Risk_n_Reward.Core.Models.SlotsModel.Symbols;

namespace Risk_n_Reward.Core.Engines.SlotsEngine;

public class SlotsEngine
{
    public SlotsResult Result(SlotsSymbols[] slots)
    {
        SlotsOutcome result = GameResult(slots);
        decimal payout = Payout(result);
        
        return new SlotsResult()
        {
            Result = result,
            PayoutMultiplier = payout,
        };
    }

    private SlotsOutcome GameResult(SlotsSymbols[] slotReels)
    {
        if (slotReels.All(r => r == SlotsSymbols.Seven))
        {
            return SlotsOutcome.Jackpot;
        }

        if (slotReels.All(r => r == SlotsSymbols.Bell))
        {
            return SlotsOutcome.BigWin;
        }

        if (slotReels.All(r => r == SlotsSymbols.ThreeCherries))
        {
            return SlotsOutcome.BigWin;
        }

        if (slotReels.All(r => r == SlotsSymbols.Cherry))
        {
            return SlotsOutcome.MediumWin;
        }

        if (slotReels.All(r => r == SlotsSymbols.Lemon))
        {
            return SlotsOutcome.MediumWin;
        }

        if (slotReels.Distinct().Count() == 2)
        {
            return SlotsOutcome.SmallWin;
        }
            
        
        return SlotsOutcome.Lose;
    }
    
    private decimal Payout(SlotsOutcome outcome)
    {
        return outcome switch
        {
            SlotsOutcome.Jackpot => 10.0m,
            SlotsOutcome.BigWin => 5.0m,
            SlotsOutcome.MediumWin => 3.0m,
            SlotsOutcome.SmallWin => 1.5m,
            _ => 0m
        };
    }
}