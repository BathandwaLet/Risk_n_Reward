using static Risk_n_Reward.Core.Engines.LuckyDiceEngine;

namespace Risk_n_Reward.Core.Models;

public class LuckyDiceResult
{
    public bool DiceMatch { get; init; }
    public bool DoubleSixes { get; init; }
    public decimal PayoutMultiplier { get; init; }

    public bool IsWin => PayoutMultiplier > 0;
}

