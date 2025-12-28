using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games.CoinToss;

public class CoinToss : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Coin Toss");
        
        Console.WriteLine($"Place your bet. You currently have {wallet.Balance} VMali.");
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
        
        Random rnd = new Random();
        var  computerChoice =  (rnd.Next(0, 2) == 0)? CoinSide.H : CoinSide.T;

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

    
}

public enum CoinSide
{
    H,
    T, 
} 