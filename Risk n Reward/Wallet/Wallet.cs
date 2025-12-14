namespace Risk_n_Reward.Wallet;

public class Wallet
{
    public decimal Balance { get; private set; }

    public Wallet(decimal startingBalance)
    {
        Balance = startingBalance;
    }

    public bool CanAfford(decimal amount)
    {
        return amount > 0 && Balance >= amount;
    }

    public void Debit(decimal amount)
    {
        if (!CanAfford(amount))
            throw new InvalidOperationException("Insufficient balance");

        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Invalid credit amount");

        Balance += amount;
    }
}