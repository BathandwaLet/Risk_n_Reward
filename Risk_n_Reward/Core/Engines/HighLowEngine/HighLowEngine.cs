using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Games.HighLow;
using Risk_n_Reward.Core.Models.HighLowModels.Outcomes;
using Risk_n_Reward.Core.Models.HighLowModels.Results;

namespace Risk_n_Reward.Core.Engines.HighLowEngine;

public class HighLowEngine
{
    
    public HighLowResult Result(Card firstCard, Card nextCard, HL player)
    {
        var actual = Actual(firstCard, nextCard);
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

        if (actual == HL.Same)
        {
            return HighLowOutcome.Draw;
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

    private static HL Actual(Card firstCard, Card nextCard)
    {
        var firstCardValue = firstCard.CalculateCardValue();
        var nextCardValue = nextCard.CalculateCardValue();

        if (nextCardValue > firstCardValue)
        {
            return HL.Higher;
        }
        else if (firstCardValue == nextCardValue)
        {
            return HL.Same;
        }

        return HL.Lower;
    }
}