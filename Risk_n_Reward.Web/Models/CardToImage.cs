using Risk_n_Reward.Core.Models.CardDeck;

namespace Risk_n_Reward.Web.Models;

public class CardToImage
{
    public static string ConvertCardToImage(string card)
    {
        return (card) switch
        {
            ("Ace of Clubs") => "images/gameassets/deck-of-cards/ace-of-clubs.png",
            ("Two of Clubs") => "images/gameassets/deck-of-cards/two-of-clubs.png",
            ("Three of Clubs") => "images/gameassets/deck-of-cards/three-of-clubs.png",
            ("Four of Clubs") => "images/gameassets/deck-of-cards/four-of-clubs.png",
            ("Five of Clubs") => "images/gameassets/deck-of-cards/five-of-clubs.png",
            ("Six of Clubs") => "images/gameassets/deck-of-cards/six-of-clubs.png",
            ("Seven of Clubs") => "images/gameassets/deck-of-cards/seven-of-clubs.png",
            ("Eight of Clubs") => "images/gameassets/deck-of-cards/eight-of-clubs.png",
            ("Nine of Clubs") => "images/gameassets/deck-of-cards/nine-of-clubs.png",
            ("Ten of Clubs") => "images/gameassets/deck-of-cards/ten-of-clubs.png",
            ("Jack of Clubs") => "images/gameassets/deck-of-cards/jack-of-clubs.png",
            ("Queen of Clubs") => "images/gameassets/deck-of-cards/queen-of-clubs.png",
            ("King of Clubs") => "images/gameassets/deck-of-cards/king-of-clubs.png",
            ("Ace of Diamonds") => "images/gameassets/deck-of-cards/ace-of-diamonds.png",
            ("Two of Diamonds") => "images/gameassets/deck-of-cards/two-of-diamonds.png",
            ("Three of Diamonds") => "images/gameassets/deck-of-cards/three-of-diamonds.png",
            ("Four of Diamonds") => "images/gameassets/deck-of-cards/four-of-diamonds.png",
            ("Five of Diamonds") => "images/gameassets/deck-of-cards/five-of-diamonds.png",
            ("Six of Diamonds") => "images/gameassets/deck-of-cards/six-of-diamonds.png",
            ("Seven of Diamonds") => "images/gameassets/deck-of-cards/seven-of-diamonds.png",
            ("Eight of Diamonds") => "images/gameassets/deck-of-cards/eight-of-diamonds.png",
            ("Nine of Diamonds") => "images/gameassets/deck-of-cards/nine-of-diamonds.png",
            ("Ten of Diamonds") => "images/gameassets/deck-of-cards/ten-of-diamonds.png",
            ("Jack of Diamonds") => "images/gameassets/deck-of-cards/jack-of-diamonds.png",
            ("Queen of Diamonds") => "images/gameassets/deck-of-cards/queen-of-diamonds.png",
            ("King of Diamonds") => "images/gameassets/deck-of-cards/king-of-diamonds.png",
            ("Ace of Hearts") => "images/gameassets/deck-of-cards/ace-of-hearts.png",
            ("Two of Hearts") => "images/gameassets/deck-of-cards/two-of-hearts.png",
            ("Three of Hearts") => "images/gameassets/deck-of-cards/three-of-hearts.png",
            ("Four of Hearts") => "images/gameassets/deck-of-cards/four-of-hearts.png",
            ("Five of Hearts") => "images/gameassets/deck-of-cards/five-of-hearts.png",
            ("Six of Hearts") => "images/gameassets/deck-of-cards/six-of-hearts.png",
            ("Seven of Hearts") => "images/gameassets/deck-of-cards/seven-of-hearts.png",
            ("Eight of Hearts") => "images/gameassets/deck-of-cards/eight-of-hearts.png",
            ("Nine of Hearts") => "images/gameassets/deck-of-cards/nine-of-hearts.png",
            ("Ten of Hearts") => "images/gameassets/deck-of-cards/ten-of-hearts.png",
            ("Jack of Hearts") => "images/gameassets/deck-of-cards/jack-of-hearts.png",
            ("Queen of Hearts") => "images/gameassets/deck-of-cards/queen-of-hearts.png",
            ("King of Hearts") => "images/gameassets/deck-of-cards/king-of-hearts.png",
            ("Ace of Spades") => "images/gameassets/deck-of-cards/ace-of-spades.png",
            ("Two of Spades") => "images/gameassets/deck-of-cards/two-of-spades.png",
            ("Three of Spades") => "images/gameassets/deck-of-cards/three-of-spades.png",
            ("Four of Spades") => "images/gameassets/deck-of-cards/four-of-spades.png",
            ("Five of Spades") => "images/gameassets/deck-of-cards/five-of-spades.png",
            ("Six of Spades") => "images/gameassets/deck-of-cards/six-of-spades.png",
            ("Seven of Spades") => "images/gameassets/deck-of-cards/seven-of-spades.png",
            ("Eight of Spades") => "images/gameassets/deck-of-cards/eight-of-spades.png",
            ("Nine of Spades") => "images/gameassets/deck-of-cards/nine-of-spades.png",
            ("Ten of Spades") => "images/gameassets/deck-of-cards/ten-of-spades.png",
            ("Jack of Spades") => "images/gameassets/deck-of-cards/jack-of-spades.png",
            ("Queen of Spades") => "images/gameassets/deck-of-cards/queen-of-spades.png",
            ("King of Spades") => "images/gameassets/deck-of-cards/king-of-spades.png",
            _ => "images/gameassets/errors/general/error-404.png"
        };
    }
    
    
}