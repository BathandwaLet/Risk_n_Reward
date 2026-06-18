using Risk_n_Reward.Core.Engines.BlackJackEngine;
using Risk_n_Reward.Core.Models.BlackJackModels.Outcomes;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Wallet;
using static Risk_n_Reward.Core.Engines.BlackJackEngine.BlackJackEngine;
using Risk_n_Reward.Games.BlackJack;

namespace Risk_n_Reward.Games.BlackJack;

public class BlackJack : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to BlackJack!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");

        const decimal minBetAmount = 50.0m;
        decimal playerBet = TryPlaceBet(minBetAmount, wallet);

        Deck deck = new Deck();
        
        List<Card> playerHand = new();
        List<Card> dealerHand = new();

        var engine = new BlackJackEngine();
        var result = engine.Result(playerHand,dealerHand,deck);

        switch (result.Outcome)
        {
            case BlackJackOutcome.Win:
                WinMessage(playerBet,result.PayoutMultiplier ,wallet);
                break;
            case BlackJackOutcome.Push:
                PushMessage(playerBet, wallet);
                break;
            case BlackJackOutcome.Lose:
                LoseMessage(wallet);
                break;
        }

    }
    
    public decimal TryPlaceBet(decimal minBetAmount,WalletService wallet)
    {
        decimal validBet;
        
        while (true)
        {
            Console.WriteLine("Place your bet: ");
            string betAmount = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(betAmount))
            {
                Console.WriteLine("You did not enter anything.");
                continue;
            }
            
            if (!decimal.TryParse(betAmount, out validBet))
            {
                Console.WriteLine("Please enter a value between 0 and 999999 as a bet amount.");
                continue;
            }
            
            if (validBet < minBetAmount)
            {
                Console.WriteLine($"Please enter an amount greater than {minBetAmount} VMali.");
                continue;
            }
            
            if (!wallet.PlaceBet(validBet))
            {
                Console.WriteLine($"Insufficient funds!");
                continue;
            }
            
            Console.WriteLine("Bet placed sucessfully!");
            return validBet;
        }
        
    }

    void WinMessage(decimal playerBet,decimal payoutMultiplier, WalletService wallet)
    {
        Console.WriteLine("YOU WIN!");
        wallet.Payout(playerBet * payoutMultiplier);
        Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
    }

    void PushMessage(decimal playerBet, WalletService wallet)
    {
        Console.WriteLine("PUSH");
        wallet.Payout(playerBet);
        Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
    }

    void LoseMessage(WalletService wallet)
    {
        Console.WriteLine("You Lose");
        Console.WriteLine($"Your new balance is {wallet.Balance} VMali");
    }
}
