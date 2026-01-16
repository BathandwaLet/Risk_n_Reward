using static Risk_n_Reward.Games.BlackJack.BlackJack;

namespace Risk_n_Reward.Games.BlackJack;

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