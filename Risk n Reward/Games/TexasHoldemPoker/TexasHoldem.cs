using Risk_n_Reward.Core.Engines.TexasHoldemEngine;
using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models.TexasHoldemModels.Outcomes.GameResult;
using Risk_n_Reward.Core.Models.TexasHoldemModels.Outcomes.HandType;

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
        
        Console.WriteLine("No more bets.");
        
        Deck deck = new Deck();

        List<Card> playerHand = new();
        List<Card> dealerHand = new();
        List<Card> communityCards = new();
        
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
            communityCards.Add(deck.Draw());
        }
        
        //prints community hand
        Console.Write("\nCommunity Cards: ");
        ShowHand(communityCards);
        
        //prints player hand
        Console.Write("\nPlayer Hand: ");
        ShowHand(playerHand);
        
        // call engine and evaluate game
        var engine = new TexasHoldemEngine();
        var result = engine.Result(playerHand,dealerHand,communityCards);
        
        Console.Clear();
        
        //Present hands and type of hand:
        //prints dealer hand
        Console.WriteLine("Dealer Hand");
        ShowHand(dealerHand);
        Console.WriteLine(ParseHandType(result.DealerHandType));
        
        //prints community card
        Console.Write("\nCommunity Cards: ");
        ShowHand(communityCards);
        
        //prints player hand
        Console.Write("\nPlayer Hand: ");
        ShowHand(playerHand);
        Console.WriteLine(ParseHandType(result.PlayerHandType));
        
        //results presentation and wallet update
        if (result.Outcome == GameResult.Win)
        {
            Console.WriteLine($"Congratulations!, You won {result.PayoutMultiplier*playerBet}VMali!");
            wallet.Payout(result.PayoutMultiplier*playerBet);
        }
        else if (result.Outcome == GameResult.Push)
        {
            Console.WriteLine($"Push.\nYour bet of {playerBet}VMali was returned.");
            wallet.Payout(result.PayoutMultiplier*playerBet);
        }
        else
        {
            Console.WriteLine("Unfortunately, you lost.");
        }
        
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

    private static string ParseHandType(THHandType hand)
    {
        return hand switch
        {
            (THHandType.RoyalFlush) => "Royal Flush",
            (THHandType.StraightFlush) => "Straight Flush",
            (THHandType.FourOfAKind) => "Four of a Kind",
            (THHandType.FullHouse) => "Full House",
            (THHandType.Flush) => "Flush",
            (THHandType.Straight) => "Straight",
            (THHandType.ThreeOfAKind) => "Three of a Kind",
            (THHandType.TwoPair) => "Two Pair",
            (THHandType.Pair) => "Pair",
            (THHandType.HighCard) => "High Card "
        };
    }
}