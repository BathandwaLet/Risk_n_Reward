using Risk_n_Reward.Core.Models.HighLowModels.Outcomes;

namespace Risk_n_Reward.Core.Models.HighLowModels.Results;

public class HighLowResult
{
    public HighLowOutcome Outcome { get; init; }
    public decimal PayoutMultiplier { get; init; }
}