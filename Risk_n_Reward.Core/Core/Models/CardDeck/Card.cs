namespace Risk_n_Reward.Core.Models.CardDeck;

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
    
    public int CalculateCardValue()
    {
        return this.Rank switch
        {
            Rank.Ace => 1,
            Rank.Jack => 11,
            Rank.Queen => 12,
            Rank.King => 13,
            _ => ((int)this.Rank) + 2
        };
    }

    public int BaccaratCardValue()
    {
        return this.Rank switch
        {
            Rank.Ace => 1,
            Rank.Ten => 0,
            Rank.Jack => 0,
            Rank.Queen => 0,
            Rank.King => 0,
            _ => ((int)this.Rank) + 2
        };
    }

}