using Risk_n_Reward.Core.Models.SlotsModel.Outcomes;
using Risk_n_Reward.Core.Models.SlotsModel.Symbols;

namespace Risk_n_Reward.Core.Models.SlotsModel.Results;

public class SlotsResult
{
    public SlotsSymbols[] ReelsOutcome { get; init; }
    public SlotsOutcome Result { get; init; } 
    public decimal PayoutMultiplier { get; init; }
    public bool IsWin => PayoutMultiplier > 0m;
}