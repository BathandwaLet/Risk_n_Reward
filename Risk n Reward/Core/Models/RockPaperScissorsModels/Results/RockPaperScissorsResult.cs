namespace Risk_n_Reward.Core.Models.RockPaperScissorsModels.Results;

public class RockPaperScissorsResult
{
    public int Result { get; init; }
    
    public decimal PayoutMultiplier { get; init; }

    public bool IsWin => PayoutMultiplier > 0;    
}

