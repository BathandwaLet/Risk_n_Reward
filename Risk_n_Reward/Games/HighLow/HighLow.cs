using Risk_n_Reward.Core.Engines.HighLowEngine;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.HighLowModels.BetTypes;
using Risk_n_Reward.Core.Models.HighLowModels.Outcomes;
using Risk_n_Reward.Core.Models.HighLowModels.Results;
using Risk_n_Reward.Core.Wallet;

namespace Risk_n_Reward.Games.HighLow;

public class HighLow : IGame
{
    public void Start(WalletService wallet)
    {
        Console.Clear();
        
        Console.WriteLine("Welcome to High Low!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");
        
        const decimal minBetAmount = 50.0m;
        decimal playerBet = TryPlaceBet(minBetAmount, wallet);
        
        Deck deck = new Deck();
        
        Console.WriteLine("Shuffling the deck");
        Thread.Sleep(1000);
        Console.Clear();
        
        Console.WriteLine("Dealing cards");
        Thread.Sleep(1000);
        Console.Clear();

        Card firstCard;
        Card nextCard;
        
        firstCard = deck.Draw();
        nextCard = deck.Draw();

        Console.WriteLine($"Your card is {firstCard} \nIs the next card higher or lower?");
        Console.WriteLine("Press H for higher or L for lower.");
        HL playerChoice = ParsePlayerChoice(Console.ReadLine().ToUpper());

        if (playerChoice == HL.Null)
        {
            throw new ArgumentException("Error \n Incorrect selection!");
        }
        
        var engine = new HighLowEngine(); 
        HighLowResult result = engine.Result(firstCard, nextCard, playerChoice);
        
        Console.WriteLine($"The next card is {nextCard}");
        
        if (result.Outcome == HighLowOutcome.Win)
        {
            wallet.Payout(result.PayoutMultiplier * playerBet);
            Console.WriteLine($"\nCONGRATULATIONS!");
            Console.WriteLine($"You won {result.PayoutMultiplier * playerBet} VMali!");
        }
        else if (result.Outcome == HighLowOutcome.Draw)
        {
            wallet.Payout(playerBet);
            Console.WriteLine("\nIt's a Draw");
            Console.WriteLine("Your bet amount was returned");
        }
        else
        {
            Console.WriteLine("\nNo win this time.");
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