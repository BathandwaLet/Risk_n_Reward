using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Models.CardDeck;

namespace Risk_n_Reward.Games.TexasHoldemPoker;

public class TexasHoldem : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Texas Hold 'em Poker!");

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

        List<Card> playerHand = new();
        List<Card> dealerHand = new();
        List<Card> communityHand = new();
        
        Console.WriteLine("Shuffling the deck");
        Thread.Sleep(1000);
        Console.Clear();
        
        Console.WriteLine("Dealing cards");
        Thread.Sleep(1000);
        Console.Clear();
        
        //draw the cards
        //player cards
        playerHand.Add(deck.Draw());
        playerHand.Add(deck.Draw());
        
        //dealer cards
        dealerHand.Add(deck.Draw());
        dealerHand.Add(deck.Draw());
        
        //community pile
        for (int i = 0; i < 5; i++)
        {
            communityHand.Add(deck.Draw());
        }
        
        //prints *card* *card* for dealer
        Console.WriteLine("Card Card");
        
        //prints community hand
        Console.Write("\nCommunity Pile: ");
        ShowHand(communityHand);
        
        //prints player hand
        Console.Write("\nPlayer Hand: ");
        ShowHand(playerHand);
        
        // call engine
        
        //results presentation and wallet update
        
        //wallet updated, show balance.
        Console.WriteLine($"Your new balance is: {wallet.Balance} VMali");
        Console.ReadKey();
    }

    void ShowHand(List<Card> hand)
    {
        foreach (Card card in hand)
        {
            Console.Write(card + " ");
        }
    }
}