using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Web.Data;
using Risk_n_Reward.Core.Models.CrashModels;
using Risk_n_Reward.Core.Engines;
using Risk_n_Reward.Core.Engines.CrashEngine;
using Risk_n_Reward.Core.Models.CrashModels.GameOutcomes;

namespace Risk_n_Reward.Web.Controllers;

public class CrashController : Controller
{
    private readonly ApplicationDbContext _db;
    private const int Id = 1;
    private const int GameId = 4;

    public CrashController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var player = await _db.Players.FindAsync(Id);
        return View(player);
    }

    [HttpPost]
    public async Task<IActionResult> Play(decimal betAmount, string cashout)
    {
        var player = await _db.Players.FindAsync(Id);
        
        //Validation block for player
        if (player == null)
        {
            return NotFound();
        }

        if (betAmount < 0.01m)
        {
            return Redirect("Index");
        }

        if (player.WalletBalance < betAmount)
        {
            return Redirect("Index");
        }

        player.WalletBalance -= betAmount;
        
        //Log WalletTransaction
        _db.WalletTransactions.Add(new WalletTransaction
        {
            PlayerId = Id,
            Type = TransactionType.Debit, //Placed bet
            Amount = betAmount,
            BalanceAfter = player.WalletBalance,
            CreatedAt = DateTime.UtcNow,
        });

        var engine = new CrashEngine();
        var cashOut = cashout == "true"? true : false;
        var crashPoint = engine.CrashPoint();
        decimal multiplier = 1.00m;

        do
        {
            multiplier *= 1.01m;
            TempData["Multiplier"] = multiplier.ToString();
        } while (multiplier <= crashPoint || cashOut == false);

        if (multiplier > crashPoint)
        {
            multiplier = -1;
        }
        
        var result = engine.Result(multiplier);
        
        
        decimal payout;
        if (result.Win)
        {
            payout = betAmount * multiplier;
            player.WalletBalance += payout; 
            
            //Log transaction after winning
            _db.WalletTransactions.Add(new WalletTransaction
            {
                PlayerId = Id,
                Type = TransactionType.Credit,
                Amount = payout,
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
        
        TempData["Win"] = result.Win;
        TempData["BetAmount"] = betAmount.ToString();
        TempData["CrashPoint"] = result.CrashPointMultiplier.ToString();
        TempData["FinalMultiplier"] = result.PayoutMultiplier.ToString();
        
        return RedirectToAction("Index");
    }
}