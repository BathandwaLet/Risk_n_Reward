using Risk_n_Reward.Core.Engines;
using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.CoinTossModels.Results;
using Risk_n_Reward.Wallet;
using static Risk_n_Reward.Games.CoinToss.ComputerToss;

namespace Risk_n_Reward.Games.CoinToss;

public class CoinToss : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Coin Toss");
        
        Console.WriteLine($"You currently have {wallet.Balance} VMali.");
        const decimal minBetAmount = 10.0m;
        decimal playerBet = TryPlaceBet(minBetAmount, wallet);
        
        Console.WriteLine("Heads(H) or Tails(T)");
        var playerInput = Console.ReadLine().ToUpper();
        CoinSide playerChoice;
        if (playerInput == "H")
        {
            playerChoice = CoinSide.H;
        }
        else if (playerInput == "T")
        {
            playerChoice = CoinSide.T;
        }
        else
        {
            throw new ArgumentException("Invalid input");
        }

        CoinSide computerChoice = ComputerToss.Computer();
        
        var engine = new CoinTossEngine();
        CoinTossResult result = engine.Result(playerChoice, computerChoice);

        Console.WriteLine($"You chose {playerChoice}");
        Console.WriteLine($"The computer chose {computerChoice}");

        if (playerChoice == computerChoice)
        {
            wallet.Payout(playerBet * 1.5m);
            Console.WriteLine($"You won! {playerBet * 1.5m}");
        }
        else
        {
            Console.WriteLine($"You lost!");
        }
        
        Console.WriteLine($"Your new balance is {wallet.Balance}");
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
    
}

public enum CoinSide
{
    H,
    T, 
} 