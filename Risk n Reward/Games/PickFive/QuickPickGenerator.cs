namespace Risk_n_Reward.Games.PickFive;
// Here lives the quick pick logic 
public class QuickPickGenerator
{
    public static int[] Generate()
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
}