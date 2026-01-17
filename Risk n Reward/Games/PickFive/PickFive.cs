using Risk_n_Reward.Core.Engines;
using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.PickFiveModels.Results;
using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games.PickFive;

public class PickFive : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Pick Five!");
        
        Console.WriteLine($"You currently have {wallet.Balance} VMali.");
        
        Console.WriteLine($"One ticket costs 10 VMali. \n press Y to continue");
        decimal playerBet = 10;

        if (Console.ReadLine()?.ToUpper() != "Y")
        {
            Console.WriteLine("Invalid input!");
        }
        
        if (!wallet.PlaceBet(playerBet))
        {
            Console.WriteLine("Insufficient Funds!");
            return;
        }
        
        Console.Clear();
        
        Console.WriteLine("Do you want to use the quick pick?:");

        int[] playerSelection = new int[6];
        SortedSet <int> playerSelectionSet =  new SortedSet<int>();

        if (Console.ReadLine()?.ToUpper() == "Y")
        {
            playerSelection = QuickPickGenerator.Generate();
        }
        else
        {
            do
            {
                Console.WriteLine("Pick a number between 1 and 50");
                int.TryParse(Console.ReadLine(), out var input);

                if (!playerSelectionSet.Add(input))
                { 
                    Console.WriteLine($"You have already picked {input}!");
                }
            } while (playerSelectionSet.Count < 5);
            
            Console.Clear();
        
            Console.WriteLine("Now it's time to select the the bonus ball");
            int.TryParse(Console.ReadLine(), out int bonusBall);
        
            playerSelectionSet.CopyTo(playerSelection);
            playerSelection[5] = bonusBall;
        }
        
        Console.Clear();
        
        Console.WriteLine("Get ready for the draw! \n Press any key to continue");
        Console.ReadKey();
        
        Console.WriteLine("Your numbers are:");
        
        foreach (var num in playerSelection)
        {
            Console.Write(num + " ");
        }
        
        Console.WriteLine("\n Let's see our Pick Five draw!");
        Thread.Sleep(2000);

        int[] computerDraw = QuickPickGenerator.Generate();

        for (int i = 0; i < 6; i++)
        {
            if (i != 5)
            {
                Console.Write(computerDraw[i] + " ");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine($"And you bonus ball is {computerDraw[i]}");
            }
            
        }
        
        var engine = new PickFiveEngine();
        PickFiveResult result = engine.Result(playerSelection, computerDraw);

        if (result.IsWin)
        {
            wallet.Payout(result.Payout);
            Console.WriteLine($"\nCONGRATULATIONS!");
            Console.WriteLine($"Matches: {result.BallMatches}");
            Console.WriteLine($"Bonus Ball: {(result.BonusBallMatch ? "YES" : "NO")}");
            Console.WriteLine($"You won {result.Payout} VMali!");
        }
        else
        {
            Console.WriteLine("\nNo win this time.");
        }

        Console.WriteLine($"Your new balance is: {wallet.Balance} VMali");
        Console.ReadKey();
        
    }
}