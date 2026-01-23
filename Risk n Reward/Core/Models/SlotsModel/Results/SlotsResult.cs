using Risk_n_Reward.Core.Models.SlotsModel.Outcomes;

namespace Risk_n_Reward.Core.Models.SlotsModel.Results;

public class SlotsResult
{
    public SlotsOutcome Result { get; init; } 
    public decimal PayoutMultiplier { get; init; }
    public bool IsWin => PayoutMultiplier > 0m;
}