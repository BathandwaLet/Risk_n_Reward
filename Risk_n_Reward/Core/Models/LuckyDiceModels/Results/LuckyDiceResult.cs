namespace Risk_n_Reward.Core.Models.LuckyDiceModels.Results;

public class LuckyDiceResult
{
    public bool DiceMatch { get; init; }
    public bool DoubleSixes { get; init; }
    public decimal PayoutMultiplier { get; init; }

    public bool IsWin => PayoutMultiplier > 0;
}

