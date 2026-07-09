using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Web.Data;
using Risk_n_Reward.Core.Core.Engines.SlotsEngine;
using Risk_n_Reward.Core.Models.SlotsModel.Outcomes;
using Risk_n_Reward.Core.Models.SlotsModel.Symbols;

namespace Risk_n_Reward.Web.Controllers;

public class SlotsController : Controller
{
    private readonly ApplicationDbContext _db;
    private const int Id = 1;
    private const int GameId = 9;

    public SlotsController(ApplicationDbContext db)
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
        var player = await _db.Players.FindAsync(Id);
        
        //Validation block for player
        if (player == null)
        {
            return NotFound();
        }

        if (betAmount < 0.01m)
        {
            return RedirectToAction("Index");
        }

        if (player.WalletBalance < betAmount)
        {
            return RedirectToAction("Index");
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

        var engine = new SlotsEngine();
        var result = engine.Result();

        decimal payout = 0;

        if (result.IsWin)
        {
            payout = betAmount * result.PayoutMultiplier;
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
            Outcome = (result.IsWin)? Outcome.Win: Outcome.Loss,
            PlayedAt = DateTime.UtcNow,
        });
        
        await  _db.SaveChangesAsync();

        string[] reel = new string [5];
        var reels = result.ReelsOutcome;

        for (int index = 0; index < 5; index++)
        {
            reel[index] = Enum.GetName(reels[index]);
        }
            
        TempData["Reels"] = System.Text.Json.JsonSerializer.Serialize(reel);
        TempData["Win"] = result.IsWin.ToString();
        TempData["Payout"] = payout.ToString();
        
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Info()
    {
        return View();
    }
}