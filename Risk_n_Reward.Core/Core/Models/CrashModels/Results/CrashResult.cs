using Risk_n_Reward.Core.Models.CrashModels.GameOutcomes;

namespace Risk_n_Reward.Core.Models;

public class CrashResult
{
    public bool  Win { get; init; }
    public decimal CrashPointMultiplier { get; init; }
    public decimal PayoutMultiplier { get; init; }
    
}