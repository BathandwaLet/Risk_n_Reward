using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games;

public class LuckyDice : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Lucky Dice");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        Console.WriteLine("Place your bet");
        decimal.TryParse(Console.ReadLine(), out var playerBet);

        if (!wallet.PlaceBet(playerBet))
        {
            Console.WriteLine("insufficient funds");
        }
        
        Console.Clear();
        
        Console.WriteLine("Rolling the Dice...");

        int [] diceRoll = DiceRoll().ToArray();
        
        Console.WriteLine($"You threw \nDice 1: {diceRoll[0]} \n Dice 2: {diceRoll[1]}");

        switch (Matches(diceRoll))
        {
            case 0:
                Console.WriteLine("You lose!");
                Console.WriteLine($"Your new balance is {wallet.Balance}");
                break;
            case 1:
                Console.WriteLine("YOU WIN!");
                wallet.Payout(playerBet * 2m);
                Console.WriteLine($"Your new balance is {wallet.Balance}");
                break;
            case 2:
                Console.WriteLine("YOU WIN!");
                wallet.Payout(playerBet * 4m);
                Console.WriteLine($"Your new balance is {wallet.Balance}");
                break;
        }
    }

    public List<int> DiceRoll()
    {
        Random rnd = new Random();
        var r = rnd.Next(1, 7);

        List<int> diceRoll = new List<int>();

        while (diceRoll.Count <= 2)
        {
            if (r != 0)
            {
                diceRoll.Add(r);
            }
        }

        return diceRoll;
    }

    public int Matches(int [] diceRoll)
    {
        int matches = 0;

        if (diceRoll[0] == diceRoll[1])
        {
            return 1;

            if (diceRoll[0] == 6)
            {
                return 2;
            }
        }

        return 0;
    }
    
    
}