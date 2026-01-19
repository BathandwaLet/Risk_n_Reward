using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Wallet;
using static Risk_n_Reward.Core.Engines.BlackJackEngine.BlackJackEngine;
using Risk_n_Reward.Games.BlackJack;

namespace Risk_n_Reward.Games.BlackJack;

public class BlackJack : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to BlackJack!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");

        Console.WriteLine("Place your bet:");
        decimal playerBet;
        if (!decimal.TryParse(Console.ReadLine(), out playerBet))
        {
            throw new ArgumentException("Invalid input!");
        }

        if (!wallet.PlaceBet(playerBet))
        {
            throw new ArgumentException("Insufficient funds!");
            return;
        }

        Deck deck = new Deck();
        
        Console.WriteLine("Shuffling the deck");
        Thread.Sleep(1000);
        Console.Clear();
        
        Console.WriteLine("Dealing cards");
        Thread.Sleep(1000);
        Console.Clear();

        List<Card> playerHand = new();
        List<Card> dealerHand = new();


        playerHand.Add(deck.Draw());
        dealerHand.Add(deck.Draw());
        playerHand.Add(deck.Draw());
        dealerHand.Add(deck.Draw());

        Console.Clear();
        ShowDealerInitialHand(dealerHand);
        Console.WriteLine($"Your current hand {playerHand[0]} {playerHand[1]} \n {CalculateHandValue(playerHand)}");

        int index = 2;
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

        if (CalculateHandValue(playerHand) > 21)
        {
            Console.WriteLine("BUST! \n You lose!");
            Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
            return;
        }

        Console.WriteLine($"Dealer's hand {dealerHand[0]} {dealerHand[1]} {CalculateHandValue(dealerHand)}");
        
        while (CalculateHandValue(dealerHand) < 17)
        {
            dealerHand.Add(deck.Draw());
        }

        if (CalculateHandValue(dealerHand) > 21)
        {
            Console.WriteLine("YOU WIN!");
            wallet.Payout(playerBet * 1.5m);
            Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        }
        else if (CalculateHandValue(playerHand) > CalculateHandValue(dealerHand))
        {
            Console.WriteLine("YOU WIN!");
            wallet.Payout(playerBet * 1.5m);
            Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        }
        else if (CalculateHandValue(playerHand) == CalculateHandValue(dealerHand))
        {
            Console.WriteLine("PUSH");
            wallet.Payout(playerBet);
            Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        }
        else
        {
            Console.WriteLine("You Lose");
            Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        }

    }
    
    void ShowDealerInitialHand(List<Card> dealerHand)
    {
        Console.WriteLine("\nDealer's Hand:");
        Console.WriteLine(dealerHand[0]);

        int visibleValue = (int)dealerHand[0].Rank;
        Console.WriteLine($"Dealer shows: {visibleValue}");

    }
    
    private static BJ ParsePlayerChoice(string input)
    {
        return input switch
        {
            "H" => BJ.Hit ,
            "S" => BJ.Stand,
            _ => BJ.Null,
        };
    }
    
}
