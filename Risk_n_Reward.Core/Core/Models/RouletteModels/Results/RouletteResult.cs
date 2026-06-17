using Risk_n_Reward.Core.Models.RouletteModels.BetTypes;
using Risk_n_Reward.Core.Models.RouletteModels.Outcomes;

namespace Risk_n_Reward.Core.Models.RouletteModels.Results;

public class RouletteResult
{
    public RouletteOutcome Outcome { get; init; }
    public int WinningNumber { get; init; }
    public decimal PayoutMultiplier { get; init; }
    public bool IsWin => PayoutMultiplier > 0m;
}