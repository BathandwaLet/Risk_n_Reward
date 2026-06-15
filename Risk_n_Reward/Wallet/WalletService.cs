namespace Risk_n_Reward.Wallet;

public class WalletService
{
    private readonly Wallet _wallet;

    public WalletService()
    {
        _wallet = new Wallet(1000); 
    }

    public decimal Balance => _wallet.Balance;

    public bool PlaceBet(decimal amount)
    {
        if (!_wallet.CanAfford(amount))
            return false;

        _wallet.Debit(amount);
        return true;
    }

    public void Payout(decimal amount)
    { 
        _wallet.Credit(amount);
    }
}