using System.Diagnostics;

namespace Risk_n_Reward.Games.RockPaperScissors;

public class ComputerRPS
{

    public RPS ComputerPick()
    {
        Random rnd = new Random();
        int r = rnd.Next(1, 4);
        RPS computerChoice = RPS.Null;
        switch(r)
        {
            case 1:
                computerChoice = RPS.Rock;
                break;
            case 2:
                computerChoice = RPS.Paper;
                break;
            case 3:
                computerChoice = RPS.Scissors;
                break;
        }

        return computerChoice;
    }

}

public enum RPS
{
    Null,
    Rock,
    Paper,
    Scissors
}