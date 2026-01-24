using Risk_n_Reward.Core.Engines.RouletteEngine;
using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Models.RouletteModels.BetTypes;
using Risk_n_Reward.Core.Models.RouletteModels.Outcomes;

namespace Risk_n_Reward.Games.Roulette;

public class Roulette : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Roulette");
        
        Console.WriteLine($"You currently have {wallet.Balance} VMali.");
        
        Console.WriteLine("Select your bet type");
        
        Console.WriteLine("Select the corresponding number for the bet type" +
                          "\n1. Straight \n2. Green(0) \n3. Red \n4. Black \n5. Odd \n6. Even \n7. First 12 (1-12) " +
                          "\n8. Second 12 (13-24) \n9. Third 12 (25-36) \n10. 1-18 \n 11. 19-36" +
                          "\n12. First Column \n13. Second Column \n14. Third Column");
        
        int playerBetTypeNumber;
        RouletteBetType playerBetType;
        
        if (!int.TryParse(Console.ReadLine(), out playerBetTypeNumber))
        {
            throw new ArgumentException("Invalid input!");
        }

        switch (playerBetTypeNumber)
        {
            case 1:
                playerBetType = RouletteBetType.Straight;
                break;
            case 2:
                playerBetType = RouletteBetType.Green;
                break;
            case 3:
                playerBetType = RouletteBetType.Red;
                break;
            case 4:
                playerBetType = RouletteBetType.Black;
                break;
            case 5:
                playerBetType = RouletteBetType.Odd;
                break;
            case 6:
                playerBetType = RouletteBetType.Even;
                break;
            case 7:
                playerBetType = RouletteBetType.First12;
                break;
            case 8:
                playerBetType = RouletteBetType.Second12;
                break;
            case 9:
                playerBetType = RouletteBetType.Third12;
                break;
            case 10:
                playerBetType = RouletteBetType.Oneto18;
                break;
            case 11:
                playerBetType = RouletteBetType.Nineteento36;
                break;
            case 12:
                playerBetType = RouletteBetType.FirstColumn;
                break;
            case 13:
                playerBetType = RouletteBetType.SecondColumn;
                break;
            case 14:
                playerBetType = RouletteBetType.ThirdColumn;
                break;
            default:
                throw new ArgumentException("Please select a number from 1 to 14");
                break;
        }
        
        
        Console.WriteLine("Place your bet:");
        
        decimal playerBet;
        if (!decimal.TryParse(Console.ReadLine(), out playerBet))
        {
            throw new ArgumentException("Invalid input!");
        }

        if (!wallet.PlaceBet(playerBet))
        {
            throw new ArgumentException("Insufficient funds!");
        }

        int playerNumber = 0;
        
        if (playerBetType == RouletteBetType.Straight)
        {
            Console.WriteLine("Select your winning number (0-36)");
            if (!int.TryParse(Console.ReadLine(), out playerNumber))
            {
                throw new ArgumentException("Invalid input!");
            }

            if (playerNumber < 0 || playerNumber > 36)
            {
                throw new ArgumentException("Select a number between 0 and 36!");
            }
        }

        var engine = new RouletteEngine();
        var result = engine.Result(playerBetType,playerNumber);
        RouletteOutcome outcome = result.Outcome;
        
        Console.Write("Spinning");
        for (int i = 0; i < 5; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        
        Console.Clear();
        
        Console.WriteLine($"The winning number is {result.WinningNumber}");
        
        if (outcome == RouletteOutcome.Win)
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
}