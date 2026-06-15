namespace Risk_n_Reward.Games.LuckyDice;

public class DiceRoll
{
    public static int [] Roll()
    {
        int i = 0;
        int[] diceRollArr = new int [2];
        
        Random rnd = new Random();

        List<int> diceRoll = new List<int>();

        while (diceRoll.Count <= 2)
        {
            var r = rnd.Next(1, 7);
            if (r != 0)
            {
                diceRoll.Add(r);
            }
        }

        diceRollArr = diceRoll.ToArray();
        
        return diceRollArr;
    }
}