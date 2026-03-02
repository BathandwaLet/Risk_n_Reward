using Risk_n_Reward.Core.Models.CrashModels.GameOutcomes;

namespace Risk_n_Reward.Core.Models;

public class CrashResult
{
    public CrashOutcomes  Outcome { get; init; }
    public decimal PayoutMultiplier { get; init; }
}