using Risk_n_Reward.Core.Models.BaccaratModels.Outcomes;
using Risk_n_Reward.Core.Models.BaccaratModels.BetTypes;
using Risk_n_Reward.Core.Models.BaccaratModels.Outcomes;
using Risk_n_Reward.Core.Models.BaccaratModels.Results;
using Risk_n_Reward.Core.Models.CardDeck;

namespace Risk_n_Reward.Core.Engines.BaccaratEngine;

public class BaccaratEngine
{
    public BaccaratResult Result(List<Card> player, List <Card> computer, BaccaratBetType betType, Deck deck)
    {
        
        BaccaratOutcome gameOutcome = PlayGame(player, computer, deck);
        decimal payoutMultiplier =  Payout(gameOutcome, betType);
        BaccaratOutcome winningSelection = Winningselection(payoutMultiplier, betType, gameOutcome);
        
        return new  BaccaratResult()
        {
            Outcome = gameOutcome,
            PayoutMultiplier = payoutMultiplier,
            WinningOutcome = winningSelection,
        };
    }

    private BaccaratOutcome PlayGame(List<Card> player, List<Card>computer, Deck deck)
    {
        player = IsNatural(DealCards(player, deck), deck);
        computer = IsNatural(DealCards(computer, deck), deck);

        int playerValue = HandValue(player);
        int computerValue = HandValue(computer);
        
        Console.Clear();
        Console.WriteLine("Player hand:");
        PrintHand(player);
        
        Console.WriteLine("Dealer hand:");
        PrintHand(computer);

        return GameOutcome(playerValue, computerValue);
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

    private List<Card> IsNatural(List<Card> card, Deck deck)
    {
        int handValue = HandValue(card);
        
        if (handValue == 8 || handValue == 9)
        {
            return card;
        }

        card.Add(deck.Draw());
        return card;
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

    private decimal Payout (BaccaratOutcome gameOutcome, BaccaratBetType betType)
    {
        
        return (gameOutcome, betType) switch
        {
            (BaccaratOutcome.PlayerWin, BaccaratBetType.Player) => 2.0m,
            (BaccaratOutcome.BankerWin, BaccaratBetType.Banker) => 1.95m,
            (BaccaratOutcome.Tie, BaccaratBetType.Tie) => 8.0m,
            _ => 0m,
        };
    }

    private List<Card> DealCards(List <Card> cards, Deck deck)
    {
        cards.Add(deck.Draw());
        cards.Add(deck.Draw());
        
        return cards;
    }

    private void PrintHand(List<Card>cards)
    {
        foreach (var card in cards)
        {
            Console.Write(card);
            Console.Write(" ");
            Thread.Sleep(1000);
        }
        Console.WriteLine($"\nScore:{HandValue(cards)}");
    }

    private BaccaratOutcome Winningselection(decimal payoutMultiplier, BaccaratBetType betType, BaccaratOutcome gameOutcome)
    {
        return (payoutMultiplier, betType, gameOutcome) switch
        {
            (0, BaccaratBetType.Player, BaccaratOutcome.BankerWin) => BaccaratOutcome.BankerWin,
            (0, BaccaratBetType.Banker, BaccaratOutcome.PlayerWin) => BaccaratOutcome.PlayerWin,
            (0, BaccaratBetType.Player, BaccaratOutcome.Tie) => BaccaratOutcome.Tie,
            (0, BaccaratBetType.Banker, BaccaratOutcome.Tie) => BaccaratOutcome.Tie,
        };
    }
}