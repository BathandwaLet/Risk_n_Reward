using Risk_n_Reward.Core.Models.BlackJackModels.Outcomes;

namespace Risk_n_Reward.Core.Models.BlackJackModels.Results;

public class BlackJackResult
{
    public BlackJackOutcome Outcome { get; init; }
    public decimal PayoutMultiplier { get; init; }
}