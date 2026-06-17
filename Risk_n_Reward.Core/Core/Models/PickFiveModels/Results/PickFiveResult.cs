namespace Risk_n_Reward.Core.Models.PickFiveModels.Results;

public class PickFiveResult
{
    public int BallMatches { get; init; }
    public bool BonusBallMatch { get; init; }
    public decimal Payout { get; init; }
    
    public bool IsWin => Payout > 0;
}