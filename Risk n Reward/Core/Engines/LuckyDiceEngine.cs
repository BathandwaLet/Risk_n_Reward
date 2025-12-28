using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Games.LuckyDice;
using static Risk_n_Reward.Core.Models.LuckyDiceResult;


namespace Risk_n_Reward.Core.Engines;

public class LuckyDiceEngine
{
    public LuckyDiceResult Result(int [] diceRoll)
    {
        bool matches  = DiceMatches(diceRoll);
        bool doubleSixes = DoubleSixesMatch(diceRoll);
        
        return new LuckyDiceResult()
        {
            DiceMatch = matches,
            DoubleSixes = doubleSixes,
            PayoutMultiplier = GamePayoutMultiplier(matches, doubleSixes),
            
        };
    }

    private bool DiceMatches(int [] draw)
    {
        if (draw[0] == draw[1])
        {
            return true;
        }

        return false;
    }

    private bool DoubleSixesMatch(int [] draw)
    {
        if (DiceMatches(draw) && draw[0] == 6)
        {
            return true;
        }

        return false;
    }

    private decimal GamePayoutMultiplier(bool match, bool doubleSix)
    {
        return (match, doubleSix) switch
        {
            (true, false) => 2.0m,
            (true, true) => 4.0m,
            _ => 0.0m
        };
    }
}