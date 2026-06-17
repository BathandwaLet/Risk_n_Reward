namespace Risk_n_Reward.WinStreak;

public class StreakMultiplier
{
    public decimal Calculate(int streak)
    {
        if (streak >= 10) return 3.0m;
        if (streak >= 5) return 2.5m;
        if (streak >= 3) return 2.0m;
        return 1.5m;
    }
}
