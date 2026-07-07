using Risk_n_Reward.Core.Core.Models.CoinTossModels.CoinSide;

namespace Risk_n_Reward.Core.Models.CoinTossModels.Results;

public class CoinTossResult
{ 
    public bool Win  { get; init; }
    public decimal Payout  { get; init; }
    public CoinSide Computer { get; init; }
    public CoinSide Player { get; init; }
}