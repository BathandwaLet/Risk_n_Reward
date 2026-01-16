using Risk_n_Reward.Games.CoinToss;

namespace Risk_n_Reward.Games.CoinToss;

public class ComputerToss
{
    public static CoinSide computer()
    {
        Random rnd = new Random();
        CoinSide  computerChoice =  (rnd.Next(0, 2) == 0)? CoinSide.H : CoinSide.T;

        return computerChoice;
    }
}