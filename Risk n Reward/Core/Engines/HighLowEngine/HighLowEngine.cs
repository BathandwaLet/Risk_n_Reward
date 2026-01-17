using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Games.HighLow;
using Risk_n_Reward.Core.Models.HighLowModels.Outcomes;
using Risk_n_Reward.Core.Models.HighLowModels.Results;

namespace Risk_n_Reward.Core.Engines.HighLowEngine;

public class HighLowEngine
{
    
    public HighLowResult Result(HL player, HL actual)
    {
        var gameResult = Outcome(player, actual);
        var payout = Payout(gameResult);
        return new HighLowResult
        {
            Outcome = gameResult,
            PayoutMultiplier = payout,
        };
    }

    private HighLowOutcome Outcome(HL player, HL actual)
    {
        if (player == actual)
        {
            return HighLowOutcome.Win;
        }

        return HighLowOutcome.Lose;
    }

    private decimal Payout(HighLowOutcome outcome)
    {
        if (outcome == HighLowOutcome.Win)
        {
            return 1.5m;
        }
        return 0;
    }
    
    public static int CalculateCardValue(Card[] cards)
    {
        int total = cards.Sum(c => c.GetValue());
        int aceCount = cards.Count(c => c.Rank == Rank.Ace);

        while (aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }

    public static HL Actual(Card[] firstCard, Card[] nextCard)
    {
        var firstCardValue = CalculateCardValue(firstCard);
        var nextCardValue = CalculateCardValue(nextCard);

        if (firstCardValue > nextCardValue)
        {
            return HL.Higher;
        }

        return HL.Lower;
    }
}