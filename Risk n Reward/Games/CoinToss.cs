using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games;

public class CoinToss : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Coin Toss");
        
        Console.WriteLine($"Place your bet. You currently have {wallet.Balance} VMali.");
        decimal.TryParse(Console.ReadLine(), out var playerBet);

        if (playerBet > wallet.Balance)
        {
            Console.WriteLine("Insufficient Funds!");
            return;
        }
        
        Console.WriteLine("Heads(H) or Tails(T)");
        var playerChoice = Console.ReadLine().ToUpper();
        
        Random rnd = new Random();
        var  computerChoice =  (rnd.Next(0, 2) == 0)? "H" : "T";

        Console.WriteLine($"You chose {playerChoice}");
        Console.WriteLine($"The computer chose {computerChoice}");

        if (playerChoice == computerChoice)
        {
            wallet.Payout(playerBet * 1.5m);
            Console.WriteLine($"You won! {playerBet * 1.5m}");
        }
        else
        {
            wallet.Payout(playerBet * -1m);
            Console.WriteLine($"You lost!");
        }
        
        Console.WriteLine($"Your new balance is {wallet.Balance}");
        Console.ReadKey();
    }

    
}