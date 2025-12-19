using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games;

public class BlackJack : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to BlackJack!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");

        Console.WriteLine("Place your bet:");
        decimal.TryParse(Console.ReadLine(), out decimal playerBet);

        if (!wallet.PlaceBet(playerBet))
        {
            Console.WriteLine("Insufficient Funds!");
            return;
        }

        Deck deck = new Deck();
        
        Console.WriteLine("Shuffling the deck");
        Thread.Sleep(1000);
        
        Console.WriteLine("Dealing cards");
        Thread.Sleep(1000);

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

    void GameLogic(int player, int computer)
    {
        
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

    void ShowDealerInitialHand(List<Card> dealerHand)
    {
        Console.WriteLine("\nDealer's Hand:");
        Console.WriteLine(dealerHand[0]);

        int visibleValue = (int)dealerHand[0].Rank;
        Console.WriteLine($"Dealer shows: {visibleValue}");

    }

    public enum Suits
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades,

    }

    public enum Rank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }


    public class Card
    {
        public Suits Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suits suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
        
        public int GetValue()
        {
            return Rank switch
            {
                Rank.Jack or Rank.Queen or Rank.King => 10,
                Rank.Ace => 11,
                _ => (int)Rank + 2
            };
        }

    }

    public class Deck
    {
        private readonly List<Card> _cards;
        private readonly Random _random = new();

        public Deck()
        {
            _cards = new List<Card>();
            GenerateDeck();
            Shuffle();
        }

        private void GenerateDeck()
        {
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        public Card Draw()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("Deck is empty");

            Card card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }
}
