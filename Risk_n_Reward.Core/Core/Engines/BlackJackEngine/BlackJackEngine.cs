using Risk_n_Reward.Core.Models.BlackJackModels.Outcomes;
using Risk_n_Reward.Core.Models.BlackJackModels.Results;
using Risk_n_Reward.Core.Models.CardDeck;

namespace Risk_n_Reward.Core.Engines.BlackJackEngine;

public class BlackJackEngine
{
    public BlackJackResult Result(List<Card> player, List<Card> computer, Deck deck)
    {
        BlackJackOutcome gameResult = PlayGame(player, computer, deck);
        decimal payout = Payout(gameResult);
        
        return new BlackJackResult 
        {
            Outcome = gameResult,
            PayoutMultiplier = payout,
        };
    }

    
    public BlackJackOutcome PlayGame(List<Card> player, List<Card> computer, Deck deck)
    {
        CardShuffle();
        player = InitialDrawCards(player,deck);
        computer = InitialDrawCards(computer, deck);
        
        ShowDealerInitialHand(computer);
        PrintHand(player,PlayerOrComputer.Player);
        
        while (true)
        {
            if (CalculateHandValue(player) > 21)
                return BlackJackOutcome.Lose;

            Console.WriteLine("Press H to hit, S to stand.");
            var playerKey = Console.ReadKey(true).Key; // blocks until a key is pressed

            if (playerKey == ConsoleKey.H)
            {
                player.Add(deck.Draw());
                PrintHand(player, PlayerOrComputer.Player);
            }
            else if (playerKey == ConsoleKey.S)
            {
                PrintHand(player, PlayerOrComputer.Player);
                PrintHand(computer, PlayerOrComputer.Computer);

                if (CalculateHandValue(computer) < 17)
                {
                    computer.Add(deck.Draw());
                    PrintHand(computer, PlayerOrComputer.Computer);
                }

                if (CalculateHandValue(computer) > 21)
                    return BlackJackOutcome.Win;

                return GameResult(CalculateHandValue(player), CalculateHandValue(computer));
            }
        }
        
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
            return 2m;
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

    private void PrintHand(List<Card> Cards, PlayerOrComputer playerOrComputer)
    {
        if (playerOrComputer == PlayerOrComputer.Player)
        {
            Console.WriteLine("\nThe player cards: ");
            foreach (var card in Cards)
            {
                Console.Write(card + " ");
            }
            Console.WriteLine(" " + CalculateHandValue(Cards));
        }

        if (playerOrComputer == PlayerOrComputer.Computer)
        {
            Console.WriteLine("\nThe dealer cards: ");
            foreach (var card in Cards)
            {
                Console.Write(card + " ");
            }
            Console.WriteLine(" " + CalculateHandValue(Cards));
        } 
        
    }
    
    public void ShowDealerInitialHand(List<Card> dealerHand)
    {
        Console.WriteLine("\nDealer's Hand:");
        Console.WriteLine(dealerHand[0]);
        
        int visibleValue = dealerHand[0].CalculateCardValue();
        Console.WriteLine($"Dealer shows: {visibleValue}");

    }

    private List<Card> InitialDrawCards(List<Card>cards, Deck deck)
    {
        for (int i = 0; i < 2; i++)
        {
            cards.Add(deck.Draw());
        }

        return cards;
    }
    
    private static void CardShuffle()
    {
        Console.WriteLine("Shuffling the deck");
        Thread.Sleep(1000);
        Console.Clear();
        
        Console.WriteLine("Dealing cards");
        Thread.Sleep(1000);
        Console.Clear();
    }

    private enum PlayerOrComputer
    {
        Player,
        Computer
    }
}
