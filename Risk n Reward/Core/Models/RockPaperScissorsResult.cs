using static Risk_n_Reward.Games.RockPaperScissors.ComputerRPS;

namespace Risk_n_Reward.Core.Models;

public class RockPaperScissorsResult
{
    public int Result { get; init; }
    
    public decimal PayoutMultiplier { get; init; }

    public bool IsWin => PayoutMultiplier > 0;    
}

