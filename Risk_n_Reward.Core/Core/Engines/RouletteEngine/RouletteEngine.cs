using Risk_n_Reward.Core.Models.RouletteModels.BetTypes;
using Risk_n_Reward.Core.Models.RouletteModels.Results;
using Risk_n_Reward.Core.Models.RouletteModels.Outcomes;

namespace Risk_n_Reward.Core.Engines.RouletteEngine;

public class RouletteEngine
{
    public RouletteResult Result(RouletteBetType playerBetType, int playerNumber)
    {
        var winningNumber = WinningNumber();
        var payoutMultiplier = Payout(playerBetType, playerNumber, winningNumber);
        RouletteOutcome outcome = (payoutMultiplier > 0) ? RouletteOutcome.Win : RouletteOutcome.Lose;
        
        return new RouletteResult()
        {
            Outcome = outcome,
            PayoutMultiplier = payoutMultiplier,
            WinningNumber = winningNumber,
        };
    }

    private int WinningNumber()
    {
        Random rnd = new Random();
        return rnd.Next(1,37);
    }

    private decimal Payout(RouletteBetType playerBetType, int playerNumber , int winningNumber)
    {
        decimal payout = 0m;
        switch (playerBetType)
        {
            case RouletteBetType.Straight:
                payout = (CheckStraight(playerNumber, winningNumber)) ? 35.0m : 0m;
                break;
            case RouletteBetType.Green:
                payout = (CheckGreenNumber(winningNumber)) ? 35.0m : 0m;
                break;
            case RouletteBetType.Red:
                payout = (CheckRedNumber(winningNumber)) ? 2.0m : 0m;
                break;
            case RouletteBetType.Black:
                payout = (CheckBlackNumber(winningNumber)) ? 2.0m : 0m;
                break;
            case RouletteBetType.Odd:
                payout = (CheckOdd(winningNumber)) ? 2.0m : 0m;
                break;
            case RouletteBetType.Even:
                payout = (CheckEven(winningNumber)) ? 2.0m : 0m;
                break;
            case RouletteBetType.First12:
                payout = (Check1st12(winningNumber)) ? 3.0m : 0m;
                break;
            case RouletteBetType.Second12:
                payout = (Check2nd12(winningNumber)) ? 3.0m : 0m;
                break;
            case RouletteBetType.Third12:
                payout = (Check3rd12(winningNumber)) ? 3.0m : 0m;
                break;
            case RouletteBetType.Oneto18:
                payout = (Check1st18(winningNumber)) ? 1.5m : 0m;
                break;
            case RouletteBetType.Nineteento36:
                payout = (Check2nd18(winningNumber)) ? 1.5m : 0m;
                break;
            case RouletteBetType.FirstColumn:
                payout = (Check1stColumn(winningNumber)) ? 3.0m : 0m;
                break;
            case RouletteBetType.SecondColumn:
                payout = (Check2ndColumn(winningNumber)) ? 3.0m : 0m;
                break;
            case RouletteBetType.ThirdColumn:
                payout = (Check3rdColumn(winningNumber)) ? 3.0m : 0m;
                break;
            default:
                break;
        }

        return payout;
    }
    private bool CheckStraight(int playerNumber, int winningNumber)
    {
        return playerNumber == winningNumber;
    }
    
    private bool CheckRedNumber(int winningNumber)
    {
        HashSet<int> redNumbers = new()
            {
                1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35
            };

        if (redNumbers.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }

    private bool CheckBlackNumber(int winningNumber)
    {
        HashSet<int> blackNumbers = new()
        {
            2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36
        };

        if (blackNumbers.Contains(winningNumber))
        {
            return true;
        }
        
        return false;
    }

    private bool CheckOdd(int winningNumber)
    {
        return winningNumber % 2 != 0;
    }

    private bool CheckEven(int winningNumber)
    {
        return winningNumber % 2 == 0;
    }
    
    private bool Check1st12(int winningNumber)
    {
        HashSet<int> first12 = new()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
        };

        if (first12.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }

    private bool Check2nd12(int winningNumber)
    {
        HashSet<int> second12 = new()
        {
            13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 
        };

        if (second12.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }

    private bool Check3rd12(int winningNumber)
    {
        HashSet<int> third12 = new()
        {
            25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36
        };

        if (third12.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }

    private bool CheckGreenNumber(int winningNumber)
    {
        if (winningNumber == 0)
        {
            return true;
        }
        
        return false;
    }

    private bool Check1st18(int winningNumber)
    {
        HashSet<int> first18 = new()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18
        };

        if (first18.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }

    private bool Check2nd18(int winningNumber)
    {
        HashSet<int> second18 = new()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18
        };

        if (second18.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }
    
    private bool Check1stColumn(int winningNumber) 
    {
        HashSet<int> firstColumn = new()
        {
            1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34
        };

        if (firstColumn.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }
    
    private bool Check2ndColumn(int winningNumber)
    {
        HashSet<int> secondColumn = new()
        {
            2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35
        };

        if (secondColumn.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }
    
    private bool Check3rdColumn(int winningNumber){
        HashSet<int> thirdColumn = new()
        {
            3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36
        };

        if (thirdColumn.Contains(winningNumber))
        {
            return true;
        }

        return false;
    }
}

