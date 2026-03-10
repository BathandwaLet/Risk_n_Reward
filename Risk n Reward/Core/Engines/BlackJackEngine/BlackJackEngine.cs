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

    /*
     * int index = 2;
       string playerChoice = "";
       
       while (CalculateHandValue(playerHand) < 21 && playerChoice != "S")
       {
           Console.WriteLine("Press H to hit, S to stand.");
           playerChoice = Console.ReadLine().ToUpper();
           
           if (playerChoice == "H")
           {
               playerHand.Add(deck.Draw());

               for (int i = 0; i <= index; i++)
               {
                   Console.Write(playerHand[i] + " ");
               }
               Console.WriteLine(CalculateHandValue(playerHand));
               Console.WriteLine();

               index++;
           }
           else
           {
               Console.WriteLine("Invalid input");
           }
       }
     */
    public BlackJackOutcome PlayGame(List<Card> player, List<Card> computer, Deck deck)
    {
        CardShuffle();
        player = InitialDrawCards(player,deck);
        computer = InitialDrawCards(computer, deck);
        
        ShowDealerInitialHand(computer);
        PrintHand(player,1);
        
        if (Console.KeyAvailable)
        {
            var playerKey = Console.ReadKey(true).Key;
            
            while (CalculateHandValue(player) <= 21 && playerKey != ConsoleKey.S)
            {
                
                if (CalculateHandValue(player) > 21)
                {
                    return BlackJackOutcome.Lose;
                }

                Console.WriteLine("Press H to hit, S to stand.");
                
                if (playerKey == ConsoleKey.H)
                {
                    player.Add(deck.Draw());
                    PrintHand(player, 1);
                }
            
                if (playerKey == ConsoleKey.S)
                {
                    PrintHand(player,1);
                    PrintHand(computer,2);
                    
                    if (CalculateHandValue(computer) < 17)
                    {
                        computer.Add(deck.Draw());
                        PrintHand(computer, 2);
                    }

                    if (CalculateHandValue(computer) > 21)
                    {
                        return BlackJackOutcome.Win;
                    }
                    
                    return GameResult(CalculateHandValue(player), CalculateHandValue(computer));
                }
            
            }
        }

        return BlackJackOutcome.Push;
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

    private void PrintHand(List<Card> Cards, int playerOrComputer)
    {
        if (playerOrComputer == 1)
        {
            Console.WriteLine("\nThe player cards: ");
            foreach (var card in Cards)
            {
                Console.Write(card + " ");
            }
            Console.WriteLine(" " + CalculateHandValue(Cards));
        }

        if (playerOrComputer == 2)
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

        int visibleValue = (int)dealerHand[0].Rank;
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
}
