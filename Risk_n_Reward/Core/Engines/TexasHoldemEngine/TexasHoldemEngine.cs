using Risk_n_Reward.Core.Models.TexasHoldemModels.Outcomes.GameResult;
using Risk_n_Reward.Core.Models.TexasHoldemModels.Outcomes.HandType;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models.TexasHoldemModels.Results;

namespace Risk_n_Reward.Core.Engines.TexasHoldemEngine;

public class TexasHoldemEngine
{
    public TexasHoldemResult Result(List<Card> player, List<Card> dealer, List<Card> communityCards)
    {
        THHandType playerHandType = EvaluateHand(player,communityCards);
        THHandType dealerHandType = EvaluateHand(dealer,communityCards);
        GameResult outcome = EvaluateGame(player,dealer,communityCards);
        decimal payoutMultiplier = PayoutMultiplier(outcome,playerHandType);
        
        return new TexasHoldemResult()
        {
            PlayerHandType = playerHandType,
            DealerHandType = dealerHandType,
            Outcome = outcome,
            PayoutMultiplier = payoutMultiplier,
        };
    }
    
    private GameResult EvaluateGame(List<Card> playerHand, List<Card> dealerHand, List<Card> communityCards)
    {
        var playerHandType = EvaluateHand(playerHand, communityCards);
        var dealerHandType = EvaluateHand(dealerHand, communityCards);

        //player win cases
        if (playerHandType > dealerHandType)
        {
            return GameResult.Win;
        }
        
        //dealer wins
        if (playerHandType < dealerHandType)
        {
            return GameResult.Lose;
        }

        //if the player and dealer have the same hand type we evaluate the hand values
        var playerHandValue = HandValue(playerHand,communityCards);
        var dealerHandValue  = HandValue(dealerHand, communityCards);

        if (playerHandValue > dealerHandValue)
        {
            return GameResult.Win;
        }

        if (playerHandValue < dealerHandValue)
        {
            return GameResult.Lose;
        }

        return GameResult.Push;
    }
    private THHandType EvaluateHand(List<Card> hand, List<Card> communityCards)
    {
        return (hand,communityCards) switch
        {
            var(h,c) when IsRoyalFlush(h,c) => THHandType.RoyalFlush,
            var(h,c) when IsStraightFlush(h,c) => THHandType.StraightFlush,
            var(h,c) when IsFourOfAKind(h,c) => THHandType.FourOfAKind,
            var(h,c) when IsFullHouse(h,c) => THHandType.FullHouse,
            var(h,c) when IsFlush(h,c) => THHandType.Flush,
            var(h,c) when IsStraight(h,c) => THHandType.Straight,
            var(h,c) when IsThreeOfAKind(h,c) => THHandType.ThreeOfAKind,
            var(h,c) when IsTwoPair(h,c) => THHandType.TwoPair,
            var(h,c) when IsPair(h,c) => THHandType.Pair,
            _ => THHandType.HighCard
        };
    }
    
    //Check Royal Flush
    private bool IsRoyalFlush(List<Card> hand, List<Card> community)
    {
        //An ace high straight Flush.
        var cards = AllCards(hand, community);
        if (IsFlush(hand, community))
        {
            if (IsStraightFlush(hand, community))
            {
                var groups = RankGroups(cards);

                var flushCards = GetFlushCards(hand, community);
                var ranks = flushCards.Select(c => (int)c.Rank).ToHashSet();

                return (ranks.Contains(10) && ranks.Contains(11) && ranks.Contains(12) &&
                    ranks.Contains(13) && ranks.Contains(14)) ;
            }
        }
        
        return false;
    }
    //Check Straight Flush
    private bool IsStraightFlush(List<Card> hand, List<Card> community)
    {
        //Five consecutive cards in the same suit.
        var cards = AllCards(hand, community);
        if (IsFlush(hand, community))
        {
            var flushCards = GetFlushCards(hand, community);
            
            if(IsStraight(flushCards, new List<Card>()))
            {
                return true;
            }
        }
        
        return false;
    }
    //Check Four of a Kind
    private bool IsFourOfAKind(List<Card> hand, List<Card> community)
    {
        //Four cards of the same rank.
        var cards = AllCards(hand, community);
        var groups = RankGroups(cards);

        return groups.Any(g => g.Value == 4);
    }
    //Check Full House
    private bool IsFullHouse(List<Card> hand, List<Card> community)
    {
        //Three cards of the same rank with two cards of another same rank
        var cards = AllCards(hand, community);
        var groups = RankGroups(cards);

        bool hasThree = groups.Any(g => g.Value >= 3);
        bool hasPair = IsPair(hand,community);

        return hasThree && hasPair;
    }
    //Check Flush
    private bool IsFlush(List<Card> hand, List<Card> community)
    {
        //Five cards in the same suit (not consecutive).
        var cards = AllCards(hand, community);
        var suits = SuitGroups(cards);

        return suits.Any(s => s.Value.Count >= 5);
    }
    //Check Straight
    private bool IsStraight(List<Card> hand, List<Card> community)
    {
        //Five consecutive cards of different suits. (Aces can count either as a high or a low card.)
        var cards = AllCards(hand, community);
        var ranks = UniqueRanks(cards);

        int consecutive = 1;

        for (int i = 1; i < ranks.Count; i++)
        {
            if (ranks[i] == ranks[i - 1] + 1)
            {
                consecutive++;
                if (consecutive >= 5)
                    return true;
            }
            else
            {
                consecutive = 1;
            }
        }

        return false;
    }
    //Check Three of a Kind
    private bool IsThreeOfAKind(List<Card> hand, List<Card> community)
    {
        //Three cards of the same rank.
        var cards = AllCards(hand, community);
        var groups = RankGroups(cards);

        return groups.Any(g => g.Value >= 3);
    }
    //Check Two Pair
    private bool IsTwoPair(List<Card> hand, List<Card> community)
    {
        //Two cards of the same rank together with two cards of another same rank.
        var cards = AllCards(hand, community);
        var groups = RankGroups(cards);

        return groups.Count(g => g.Value >= 2) >= 2;
    }
    //Check Pair
    private bool IsPair(List<Card> hand, List<Card> community)
    {
        //Two cards of the same rank.
        var cards = AllCards(hand, community);
        var groups = RankGroups(cards);

        return groups.Any(g => g.Value >= 2);
    }
    //Else High Card
    private int HandValue(List<Card> hand, List<Card> community)
    {
        int sum = 0;
        // calculations of the highest five cards...
        var cards = AllCards(hand, community);
        var ranks = UniqueRanks(cards);
        var bestFive = cards.OrderByDescending(c => c.Rank).Take(5).ToList();

        foreach (var card in bestFive)
        {
            sum += (int)card.Rank;
        }
        
        return sum;
    }
    
    // helper functions to combine the cards(hands + community). 
    private List<Card> AllCards(List<Card> hand, List<Card> communityCards)
    {
        return hand.Concat(communityCards).ToList();
    }

    //method to return the cards that make up a flush
    private List<Card> GetFlushCards(List<Card> hand, List<Card> community)
    {
        var cards = AllCards(hand, community);

        var suitGroup = cards.GroupBy(c => c.Suit).FirstOrDefault(g =>
            g.Count() <= 5);
        
        return suitGroup.ToList();
    }

    //method to sort the cards according to rank and group where needed
    private Dictionary<int, int> RankGroups(List<Card> cards)
    {
        return cards
            .GroupBy(c => (int)c.Rank)
            .ToDictionary(g => g.Key, g => g.Count());
    }
    
    //method to sort the cards according to suit and group
    private Dictionary<Suits, List<Card>> SuitGroups(List<Card> cards)
    {
        return cards
            .GroupBy(c => c.Suit)
            .ToDictionary(g => g.Key, g => g.ToList());
    }
    
    //method to sort into unique ranks
    private List<int> UniqueRanks(List<Card> cards)
    {
        var ranks = cards
            .Select(c => (int)c.Rank)
            .Distinct()
            .OrderBy(r => r)
            .ToList();

        // Ace low straight support (A=14 → also 1)
        if (ranks.Contains(14))
            ranks.Insert(0, 1);

        return ranks;
    }
    
    //method that returns the appropriate payout multiplier
    private decimal PayoutMultiplier(GameResult gameResult, THHandType player)
    {
        //win cases
        if (gameResult == GameResult.Win)
        {
            return (player) switch
            {
                (THHandType.RoyalFlush) => 100m,
                (THHandType.StraightFlush) => 50m,
                (THHandType.FourOfAKind) => 20m,
                (THHandType.FullHouse) => 7m,
                (THHandType.Flush) => 5m,
                (THHandType.Straight) => 4m,
                (THHandType.ThreeOfAKind) => 3m,
                (THHandType.TwoPair) => 2m,
                _ => 1m
            };
        }

        //lose cases
        if (gameResult == GameResult.Lose)
        {
            return 0m;
        }

        //push
        return 1m;
    }
}