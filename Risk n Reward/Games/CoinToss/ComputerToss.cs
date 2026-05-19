using Risk_n_Reward.Games.CoinToss;

namespace Risk_n_Reward.Games.CoinToss;

public class ComputerToss
{
    public static CoinSide Computer()
    {
        Random rnd = new Random();
        CoinSide  computerChoice =  (rnd.Next(0, 2) == 0)? CoinSide.Heads : CoinSide.Tails;

        return computerChoice;
    }
}