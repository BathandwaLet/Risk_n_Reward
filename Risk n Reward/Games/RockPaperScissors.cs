using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games;

public class RockPaperScissors : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Rock, Paper , Scissors!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");

        Console.WriteLine("Place your bet:");
        decimal.TryParse(Console.ReadLine(), out decimal playerBet);

        if (!wallet.PlaceBet(playerBet))
        {
            Console.WriteLine("Insufficient Funds!");
            return;
        }
        
        Console.WriteLine($"Press R for rock, P for Paper and S for Scissors.");
        string playerChoice = Console.ReadLine();

        Random rnd = new Random();
        string computerChoice = "";

        switch (rnd.Next(1, 4))
        {
            case 1:
                computerChoice = "R";
                break;
            case 2:
                computerChoice = "P";
                break;
            case 3:
                computerChoice = "S";
                break;
            default: break;
        }

        switch (GameLogic(playerChoice, computerChoice))
        {
            case -1:
                Console.WriteLine("You Lose");
                Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
                break;
            case 0:
                Console.WriteLine("Draw");
                wallet.Payout(playerBet);
                Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
                break;
            case 1:
                Console.WriteLine("YOU WIN!");
                wallet.Payout(playerBet * 1.5m);
                Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
                break;
            default: break;
        }

    }

    public static int GameLogic(string player, string computer)
    {
        if (player == "R" && computer == "S" || player == "S" && computer == "P" || player == "P" && computer == "R")
        {
            return 1;
        }
        else if (player == computer)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }
}