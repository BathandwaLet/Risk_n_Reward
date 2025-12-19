using Risk_n_Reward.Wallet;

namespace Risk_n_Reward.Games;

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
            playerSelection = QuickPick();
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

        int[] computerDraw = QuickPick();

        for (int i = 0; i < 6; i++)
        {
            if (i == 5)
            {
                Console.WriteLine($"And you bonus ball is {computerDraw[i]}");
            }
            else
            {
                Console.Write(computerDraw[i] + " ");
                Thread.Sleep(2000);
            }
            
        }

        if (Payout(playerSelection, computerDraw) > 0)
        {
            Console.WriteLine($"CONGRATULATIONS! \n YOU HAVE WON {Payout(playerSelection,computerDraw)} VMali!");
            wallet.Payout(Payout(playerSelection,computerDraw));
        }
        else
        {
            Console.WriteLine("Unlucky this time.");
        }
        
        Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
        Console.ReadKey();
    }

    public static int[] QuickPick()
    {
        SortedSet<int> quickPick = new SortedSet<int>();
        
        Random rnd = new Random();

        int number = 0;
        
        do
        {
            number = rnd.Next(1, 51);

            if (number != 0)
            {
                quickPick.Add(number);
            }
        } while (quickPick.Count < 5);

        int index = 0;
        int[] quickPickArr = new  int [6];
        
        foreach (int num in quickPick)
        {
            quickPickArr[index] = num;
            index++;
        }

        number = 0;
        
        while (number == 0)
        {
            number = rnd.Next(1, 51);
            if (number != 0)
            {
                quickPickArr[5] = number;
            }
        }

        return quickPickArr;
    }

    public int Matches(int [] player, int [] computer)
    {
        int matches = 0;
        
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (player[i] == computer[j])
                {
                    matches++;
                }
            }
        }
        
        return matches;
    }

    public int BonusBallMatch(int [] player, int [] computer)
    {
        if (player[5] == computer[5])
        {
            return 1;
        }
        return 0;
    }

    public decimal Payout(int [] player, int [] computer)
    {
        decimal payout = 0;
        
        switch (Matches(player, computer))
        {
            case 0:
                if (BonusBallMatch(player,computer) == 1)
                {
                    payout = 1000;
                }
                break;
            case 3:
                if (BonusBallMatch(player,computer) == 1)
                {
                    payout = 25000;
                }
                else
                {
                    payout = 15000;
                }
                break;
            case 4:
                if (BonusBallMatch(player,computer) == 1)
                {
                    payout = 75000;
                }
                else
                {
                    payout = 50000;
                }
                break;
            case 5:
                if (BonusBallMatch(player,computer) == 1)
                {
                    payout = 150000;
                }
                else
                {
                    payout = 75000;
                }
                break;
            default: break;
        }

        return payout;
    }
}