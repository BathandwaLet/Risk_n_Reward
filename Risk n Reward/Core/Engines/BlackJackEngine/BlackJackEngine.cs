using Risk_n_Reward.Core.Models.BlackJackModels.Outcomes;
using Risk_n_Reward.Core.Models.BlackJackModels.Results;
using Risk_n_Reward.Core.Models.CardDeck;

namespace Risk_n_Reward.Core.Engines.BlackJackEngine;

public class BlackJackEngine
{
    public BlackJackResult Result(List<Card> player, List<Card> computer, decimal playerBet)
    {
        int playerValue = CalculateHandValue(player);
        int computerValue = CalculateHandValue(computer);
        BlackJackOutcome gameResult = GameResult(playerValue, computerValue);
        
        decimal payout = Payout(gameResult);
        
        return new BlackJackResult 
        {
            Outcome = gameResult,
            PayoutMultiplier = payout,
        };
    }

    private BlackJackOutcome GameResult(int player, int computer)
    {
        if (player > 21) 
        {
            return BlackJackOutcome.Lose;
        }
        else if (computer > 21) 
        {
            return BlackJackOutcome.Win;
        }
        else if (player > computer) 
        {
            return BlackJackOutcome.Win;
        }
        else if (player == computer) 
        {
            return BlackJackOutcome.Push;
        }

        return BlackJackOutcome.Lose;
    }


    private decimal Payout(BlackJackOutcome outcome)
    {
        if (outcome == BlackJackOutcome.Win)
        {
            return 1.5m;
        }
        else if (outcome == BlackJackOutcome.Push)
        {
            return 1.0m;
        }

        return 0m;
    }
    
    public static int CalculateHandValue(List<Card> hand)
    {
        int total = hand.Sum(c => c.GetValue());
        int aceCount = hand.Count(c => c.Rank == Rank.Ace);

        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }
    
}
