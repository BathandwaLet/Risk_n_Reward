using Risk_n_Reward.Core.Engines.BaccaratEngine;
using Risk_n_Reward.Core.Wallet;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models.BaccaratModels.BetTypes;
using Risk_n_Reward.Core.Models.BaccaratModels.Outcomes;

namespace Risk_n_Reward.Games.Baccarat;

public class Baccarat : IGame
{
    public void Start(WalletService wallet)
    {
        Console.Clear();
        
        Console.WriteLine("Welcome to Baccarat!");
        
        Console.WriteLine("Select the corresponding number for the bet type" +
                          "\n1. Player \n2. Banker \n3. Tie");
        
        int playerBetChoiceNumber = 0;
        BaccaratBetType betType = BaccaratBetType.Null;

        try
        {
            if (!int.TryParse(Console.ReadLine(), out playerBetChoiceNumber))
            {
                throw new ArgumentException("Invalid input!");
            }
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid input!");
        }

        try
        {
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
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Please select a number from 1 to 3");
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

        var engine = new BaccaratEngine();
        var result = engine.Result(player, dealer, betType, deck);

        decimal payoutMultiplier = result.PayoutMultiplier;
        BaccaratOutcome winningSelection = result.WinningOutcome;
        BaccaratOutcome gameOutcome = result.Outcome;
        bool isWin = result.IsWin;
        
        Results(gameOutcome, winningSelection, wallet, playerBet, payoutMultiplier, isWin);

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
    
    private string ParseWininingSelection(BaccaratOutcome winningSelection)
    {
        return (winningSelection) switch
        {
            (BaccaratOutcome.PlayerWin) =>"Player",
            (BaccaratOutcome.BankerWin) =>"Banker",
            (BaccaratOutcome.Tie) =>"Tie",
        };
    }

    private void Results(BaccaratOutcome outcome, BaccaratOutcome winningSelection, WalletService wallet, decimal playerBet, decimal payoutMultiplier, bool isWin)
    {
        
        switch (isWin)
        {
            case true:
                WinningMessage(playerBet, payoutMultiplier, wallet);
                break;
            case false:
                LossingMessage(winningSelection);
                break;
            
        }
        
        Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    private void WinningMessage(decimal playerBet, decimal payoutMultiplier, WalletService wallet)
    {
        Console.WriteLine($"CONGRATULATIONS!\nYou won {playerBet * payoutMultiplier}VMali!");
        wallet.Payout(playerBet * payoutMultiplier);
        
    }

    private void LossingMessage(BaccaratOutcome winningSelection)
    {
        Console.WriteLine($"The winning bet selction was {ParseWininingSelection(winningSelection)}.");
        Console.WriteLine("No win this time");
    }
    
}