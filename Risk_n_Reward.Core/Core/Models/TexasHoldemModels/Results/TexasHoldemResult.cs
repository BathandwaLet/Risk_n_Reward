using Risk_n_Reward.Core.Models.TexasHoldemModels.Outcomes.HandType;
using Risk_n_Reward.Core.Models.TexasHoldemModels.Outcomes.GameResult;

namespace Risk_n_Reward.Core.Models.TexasHoldemModels.Results;

public class TexasHoldemResult
{
    public THHandType PlayerHandType { get; init; }
    public THHandType DealerHandType { get; init; }
    public GameResult Outcome { get; init; }
    public decimal PayoutMultiplier { get; init; }
}