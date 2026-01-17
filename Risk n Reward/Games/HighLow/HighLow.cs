using Risk_n_Reward.Core.Engines.HighLowEngine;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.HighLowModels.Results;
using Risk_n_Reward.Wallet;



namespace Risk_n_Reward.Games.HighLow;

public class HighLow : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to High Low!");

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

        Card[] firstCard = new Card[1];
        Card[] nextCard = new Card[1];
        
        firstCard[0] = deck.Draw();
        nextCard[0] = deck.Draw();

        Console.WriteLine($"Your card is {firstCard[0]} \nIs the next card higher or lower?");
        Console.WriteLine("Press H for higher or L for lower.");
        HL playerChoice = ParsePlayerChoice(Console.ReadLine().ToUpper());

        if (playerChoice == HL.Null)
        {
            throw new ArgumentException("Error \n Incorrect selection!");
        }
        
        var engine = new HighLowEngine(); 
        HighLowResult result = engine.Result(playerChoice,actualChoice);

    }

    private static HL ParsePlayerChoice(string input)
    {
        return input switch
        {
            "H" => HL.Higher ,
            "L" => HL.Lower,
            _ => HL.Null
        };
    }
    
}