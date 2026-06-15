using Risk_n_Reward.Games.BlackJack;

namespace Risk_n_Reward.Core.Models.CardDeck;

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
        {
            throw new InvalidOperationException("Deck is empty");
        }

        Card card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }
}