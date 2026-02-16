using Risk_n_Reward.Core.Models.BaccaratModels.Outcomes;
using Risk_n_Reward.Core.Models.BaccaratModels.BetTypes;
using Risk_n_Reward.Core.Models.BaccaratModels.Outcomes;
using Risk_n_Reward.Core.Models.BaccaratModels.Results;
using Risk_n_Reward.Core.Models.CardDeck;

namespace Risk_n_Reward.Core.Engines.BaccaratEngine;

public class BaccaratEngine
{
    public BaccaratResult Result(List<Card> player, List<Card> dealer, decimal playerBet, BaccaratBetType betType)
    {
        
        return new  BaccaratResult();
        {
            
        }
    }
    
    private int HandValue(List<Card> hand)
    {
        int sum = 0;
        
        foreach (var card in hand)
        {
            sum += card.BaccaratCardValue();
        }
        
        sum %= 10;
        return sum;
    }

    public bool Natural(int handValue)
    {
        if (handValue == 8 || handValue == 9)
        {
            return true;
        }
        
        return false;
    }
    
    
    private BaccaratOutcome GameOutcome (int player, int dealer) 
    {
        if (player>dealer) 
        {
            return BaccaratOutcome.PlayerWin;
        }
        else if (player<dealer)
        {
            return BaccaratOutcome.BankerWin;
        }
        
        return BaccaratOutcome.Tie;
    }

    private decimal PayoutMultiplier (BaccaratOutcome gameOutcome, BaccaratBetType betType)
    {
        
        return (gameOutcome, betType) switch
        {
            (BaccaratOutcome.PlayerWin, BaccaratBetType.Player) => 2.0m,
            (BaccaratOutcome.BankerWin, BaccaratBetType.Banker) => 1.95m,
            (BaccaratOutcome.Tie, BaccaratBetType.Tie) => 8.0m,
            _ => 0m,
        };
    }
}