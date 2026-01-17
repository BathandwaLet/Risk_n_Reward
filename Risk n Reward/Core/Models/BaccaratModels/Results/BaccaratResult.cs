using Risk_n_Reward.Core.Models.BaccaratModels.Outcomes;
namespace Risk_n_Reward.Core.Models.BaccaratModels.Results;

public class BaccaratResult
{
    public BaccaratOutcome Outcome { get; init; }
    public decimal PayoutMultiplier { get; init; }
}