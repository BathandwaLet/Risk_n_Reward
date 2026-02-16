using Risk_n_Reward.Core.Engines.BaccaratEngine;
using Risk_n_Reward.Wallet;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models.BaccaratModels.BetTypes;

namespace Risk_n_Reward.Games.Baccarat;

public class Baccarat : IGame
{
    public void Start(WalletService wallet)
    {
        Console.WriteLine("Welcome to Baccarat!");

        Console.WriteLine($"You currently have {wallet.Balance} VMali.");
        
        Console.WriteLine("Select the corresponding number for the bet type" +
                          "\n1. Player \n2. Banker \n3. Tie");
        int playerBetChoiceNumber;
        BaccaratBetType betType;
        if (int.TryParse(Console.ReadLine(), out playerBetChoiceNumber))
        {
            throw new ArgumentException("Invalid input!");
        }

        switch (playerBetChoiceNumber)
        {
            case 1:
                betType = BaccaratBetType.Player;
                break;
            case 2:
                betType = BaccaratBetType.Banker;
                break;
            case 3:
                betType = BaccaratBetType.Tie;
                break;
            default:
                throw new ArgumentException("Please select a number from 1 to 3");
        }
        
        Console.WriteLine("Place your bet:");
        decimal playerBet;
        if (!decimal.TryParse(Console.ReadLine(), out playerBet))
        {
            throw new ArgumentException("Invalid input!");
        }

        if (!wallet.PlaceBet(playerBet))
        {
            throw new ArgumentException("Insufficient funds!");
        }
        
        
        
        Console.WriteLine("Shuffling the deck");
        Thread.Sleep(1000);
        Console.Clear();
        
        Console.WriteLine("Dealing cards");
        Thread.Sleep(1000);
        Console.Clear();
        
        Deck deck = new Deck();
        
        List<Card> player = new();
        List<Card> dealer = new();
        
        //player and dealer draws
        player.Add(deck.Draw());
        player.Add(deck.Draw());
        dealer.Add(deck.Draw());
        dealer.Add(deck.Draw());

        //var engine = new BaccaratEngine();

        //conclude game results and update wallet where necessary
    }
}