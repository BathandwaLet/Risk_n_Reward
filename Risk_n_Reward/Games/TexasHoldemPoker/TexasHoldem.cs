using Risk_n_Reward.Core.Engines.TexasHoldemEngine;
using Risk_n_Reward.Core.Wallet;
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

        const decimal minBetAmount = 0.01m;
        decimal playerBet = TryPlaceBet(minBetAmount, wallet);
        
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
        
        playerHand.Add(deck.Draw());
        playerHand.Add(deck.Draw());
        
        dealerHand.Add(deck.Draw());
        dealerHand.Add(deck.Draw());
        
        for (int i = 0; i < 5; i++)
        {
            communityCards.Add(deck.Draw());
        }
        
        Console.Write("\nCommunity Cards: ");
        ShowHand(communityCards);
        
        Console.Write("\nPlayer Hand: ");
        ShowHand(playerHand);
        
        var engine = new TexasHoldemEngine();
        var result = engine.Result(playerHand,dealerHand,communityCards);
        
        Console.Clear();
        
        Console.Write("Dealer Hand: ");
        ShowHand(dealerHand);
        Console.WriteLine($"\nHand type: {ParseHandType(result.DealerHandType)}");
        
        Console.Write("\nCommunity Cards: ");
        ShowHand(communityCards);
        
        
        Console.Write("\nPlayer Hand: ");
        ShowHand(playerHand);
        Console.WriteLine($"\nHand type: {ParseHandType(result.PlayerHandType)}");
        
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
        
        Console.WriteLine($"Your new balance is: {wallet.Balance} VMali");
        Console.ReadKey();
    }

    public decimal TryPlaceBet(decimal minBetAmount,WalletService wallet)
    {
        decimal validBet;
        
        while (true)
        {
            Console.WriteLine("Place your bet: ");
            string betAmount = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(betAmount))
            {
                Console.WriteLine("You did not enter anything.");
                continue;
            }
            
            if (!decimal.TryParse(betAmount, out validBet))
            {
                Console.WriteLine("Please enter a value between 0 and 999999 as a bet amount.");
                continue;
            }
            
            if (validBet < minBetAmount)
            {
                Console.WriteLine($"Please enter an amount greater than {minBetAmount} VMali.");
                continue;
            }
            
            if (!wallet.PlaceBet(validBet))
            {
                Console.WriteLine($"Insufficient funds!");
                continue;
            }
            
            Console.WriteLine("Bet placed sucessfully!");
            return validBet;
        }
        
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