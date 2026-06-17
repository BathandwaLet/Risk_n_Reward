using Risk_n_Reward.Core;

namespace Risk_n_Reward.WinStreak;

public class WinStreak
{
    public int Current { get; private set; }

    public void RecordWin() => Current++;
    public void Reset() => Current = 0;
}