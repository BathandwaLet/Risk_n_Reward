using Risk_n_Reward.Core.Results;
//using Risk_n_Reward.Games.CoinToss;
using Risk_n_Reward.Core.Models;
using Risk_n_Reward.Core.Models.CoinTossModels.Outcomes;
using Risk_n_Reward.Core.Models.CoinTossModels.Results;
using Risk_n_Reward.Core.Core.Models.CoinTossModels.CoinSide;

namespace Risk_n_Reward.Core.Engines;

public class CoinTossEngine
{
    public CoinTossResult Result(CoinSide playerChoice)
    {
        CoinSide computerChoice = ComputerSelection();
        
        bool result = GameResult(playerChoice, computerChoice) == CoinTossOutcomes.Win ? true : false;
        decimal payoutMultiplier = Payout(result);
        
        return new CoinTossResult 
        {
            Win = result,
            Payout = payoutMultiplier,
        };
    }

    /*public bool PlayGame(CoinSide playerChoice,CoinSide computerChoice)
    {
        PlayGameScript(1);
        do
        {
            if (Console.KeyAvailable)
            {
                
                computerChoice =  ComputerSelection();
            
                var key = Console.ReadKey(true).Key;
            
                if (key == ConsoleKey.H)
                {
                    playerChoice = CoinSide.Heads;
                
                }
                else if (key == ConsoleKey.T)
                {
                    playerChoice = CoinSide.Tails;
                }
                else if (key != ConsoleKey.H || key != ConsoleKey.T)
                {
                    PlayGameScript(2);    
                }
            
            }
        } while (playerChoice == CoinSide.Null || computerChoice == CoinSide.Null);

        PlayGameScript(1,playerChoice,computerChoice);
        PlayGameScript(2,playerChoice,computerChoice);
        
        bool result = (GameResult(playerChoice, computerChoice) == CoinTossOutcomes.Win )? true: false;
        
        return result;
    }*/
    
    private CoinTossOutcomes GameResult(CoinSide player, CoinSide computer)
    {
        if (player == computer)
        {
            return CoinTossOutcomes.Win;
        }
        else
        {
            return CoinTossOutcomes.Lose;
        }
    }

    private decimal Payout(bool gameResult)
    {
        if (gameResult)
        {
            return 1.5m;
        }

        return 0m;
    }
    
    public static CoinSide ComputerSelection()
    {
        Random rnd = new Random();
        CoinSide  computerChoice =  (rnd.Next(0, 2) == 0)? CoinSide.Heads : CoinSide.Tails;

        return computerChoice;
    }

    /*private void PlayGameScript(int messageNumber)
    {
        switch (messageNumber)
        {
            case 1: Console.WriteLine("Please select H for heads of T for Tails"); break;
            case 2: Console.WriteLine("Invalid input!\nPlease select H for heads of T for Tails"); break;
        }
    }*/
    
    /*private void PlayGameScript(int messageNumber, CoinSide playerChoice, CoinSide computerChoice)
    {
        switch (messageNumber)
        {
            case 1: Console.WriteLine($"You chose {playerChoice}"); break;
            case 2: Console.WriteLine($"The computer chose {computerChoice}"); break;
        }
    }*/
}