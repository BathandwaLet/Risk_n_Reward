using Risk_n_Reward.Core.Engines.BaccaratEngine;
using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models.BaccaratModels.BetTypes;
using Risk_n_Reward.Core.Models.BaccaratModels.Outcomes;

namespace Risk_n_Reward.Games.Baccarat;

public class Baccarat : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Baccarat!");
        
        Console.WriteLine("Select the corresponding number for the bet type" +
                          "\n1. Player \n2. Banker \n3. Tie");
        
        int playerBetChoiceNumber;
        BaccaratBetType betType;
        if (!int.TryParse(Console.ReadLine(), out playerBetChoiceNumber))
        {
            throw new ArgumentException("Invalid input!");
        }

        switch (playerBetChoiceNumber)
        {
            case 1:
                betType = BaccaratBetType.Player;
                break;
            case 2:
                betType = BaccaratBetType.Banker;
                break;
            case 3:
                betType = BaccaratBetType.Tie;
                break;
            default:
                throw new ArgumentException("Please select a number from 1 to 3");
        }
        
        Console.WriteLine($"You currently have {wallet.Balance} VMali.");
        const decimal minBetAmount = 50.0m;
        decimal playerBet = TryPlaceBet(minBetAmount, wallet);
        
        
        Console.WriteLine("Shuffling the deck");
        Thread.Sleep(1000);
        Console.Clear();
        
        Console.WriteLine("Dealing cards");
        Thread.Sleep(1000);
        Console.Clear();
        
        Deck deck = new Deck();
        
        List<Card> player = new();
        List<Card> dealer = new();
        
        /*
        player.Add(deck.Draw());
        player.Add(deck.Draw());
        dealer.Add(deck.Draw());
        dealer.Add(deck.Draw());
        */

        var engine = new BaccaratEngine();

        /*
         int playerHandValue = engine.HandValue(player);
        int dealerHandValue = engine.HandValue(dealer);
        
        if (playerHandValue <= 5)
        {
            player.Add(deck.Draw());
            playerHandValue = engine.HandValue(player);
        }

        if (dealerHandValue <= 5)
        {
            dealer.Add(deck.Draw());
            dealerHandValue = engine.HandValue(dealer);
        }
        */
        
        var result = engine.Result(playerHandValue, dealerHandValue,betType);
        
        Console.Clear();
        
        Console.WriteLine("Player hand:");
        foreach (var card in player)
        {
            Console.Write(card);
            Console.Write(" ");
            Thread.Sleep(1000);
        }
        Console.WriteLine($"\nScore:{playerHandValue}");
        
        Console.WriteLine();
        Console.WriteLine("Dealer hand:");
        foreach (var card in dealer)
        {
            Console.Write(card);
            Console.Write(" ");
            Thread.Sleep(1000);
        }
        Console.WriteLine($"\nScore:{dealerHandValue}");
        
        
        Console.WriteLine($"The winning bet selction was {WininingSelection(result.Outcome)}.");
        
        if (result.IsWin)
        {
            Console.WriteLine($"CONGRATULATIONS!\nYou won {playerBet * result.PayoutMultiplier}VMali!");
            wallet.Payout(playerBet * result.PayoutMultiplier);
        }
        else
        {
            Console.WriteLine("No win this time");
        }
        
        Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        Console.WriteLine("Press any key to continue.");
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
    
    private string WininingSelection(BaccaratOutcome outcome)
    {
        return (outcome) switch
        {
            (BaccaratOutcome.PlayerWin) =>"Player",
            (BaccaratOutcome.BankerWin) =>"Banker",
            (BaccaratOutcome.Tie) =>"Tie",
        };
    }
}