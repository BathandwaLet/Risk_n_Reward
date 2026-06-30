using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Core;
using Risk_n_Reward.Core.Core.Models.CoinTossModels.CoinSide;
using Risk_n_Reward.Core.Engines;
using Risk_n_Reward.Web.Data;

namespace Risk_n_Reward.Web.Controllers;

public class CoinTossController : Controller
{
    private readonly ApplicationDbContext _db;
    private const int Id = 1;
    private const int GameId = 3;

    public CoinTossController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var player = await _db.Players.FindAsync(Id);
        return View(player);
    }

    [HttpPost]
    public async Task<IActionResult> Play(decimal betAmount)
    {
        //Fetch player object from database
        var player = await _db.Players.FindAsync(Id);
        
        //if player is not in db return not found
        if (player == null)
        {
            return NotFound();
        }

        //if the bet amount is less than the minimum betAmount
        if (betAmount < 10.0m)
        {
            return RedirectToAction("Index");
        }

        //if the player does not have the enough money to place the bet
        if (player.WalletBalance < betAmount)
        {
            return RedirectToAction("Index");
        }
        
        //Debit bet amount from  player wallet
        player.WalletBalance -= betAmount;
        
        //Log the transaction in the player wallet
        _db.WalletTransactions.Add(new WalletTransaction
        {
            PlayerId = Id,
            Type = TransactionType.Debit, //Placed bet
            Amount = betAmount,
            BalanceAfter = player.WalletBalance,
            CreatedAt = DateTime.UtcNow,
        });
        
        //Call engine
        var playerChoice = coinSide == "Heads" ? CoinSide.Heads : CoinSide.Tails; 
        var engine = new CoinTossEngine();
        var result = engine.Result();
        
        //Payout to the wallet upon a win
        decimal payout;
        if (result.Win)
        {
            //payout = result.Payout; //Engine must return the payout multiplier Do That skhokho
            payout = betAmount * 1.5m; // Rememeber to implement winstreak logic once winstrreak is impleemented!!!
            player.WalletBalance += payout; 
            
            //Log transaction after winning
            _db.WalletTransactions.Add(new WalletTransaction
            {
                PlayerId = Id,
                Type = TransactionType.Credit,
                Amount = betAmount,
                BalanceAfter = player.WalletBalance,
                CreatedAt = DateTime.UtcNow,
            });
        }
        
        //Log GameSession
        _db.GameSessions.Add(new GameSession
        {
            PlayerId = Id,
            BetAmount = betAmount,
            GameId = GameId,
            Outcome = (result.Win)? Outcome.Win: Outcome.Loss,
            PlayedAt = DateTime.UtcNow,
        });
        
        await  _db.SaveChangesAsync();
        
        TempData["Win"] =  result.Win;
        TempData["Payout"] = result.Payout;
        TempData["BetAmount"] = betAmount;

        return RedirectToAction("Index");
    }
}