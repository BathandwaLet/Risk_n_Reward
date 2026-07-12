using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Core;
using Risk_n_Reward.Core.Engines;
using Risk_n_Reward.Web.Data;

namespace Risk_n_Reward.Web.Controllers;

public class PickFiveController : Controller 
{
    private readonly ApplicationDbContext _db;
    private const int Id = 1;
    private const int GameId = 7;
    
    public PickFiveController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<IActionResult> Index()
    {
        var player = await _db.Players.FindAsync(Id);
        return View(player);
    }

    public async Task<IActionResult> Play(decimal betAmount, bool quickPick)
    {
        var player = await _db.Players.FindAsync(Id);
    
        if (player == null)
        {
            return NotFound();
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
        
        var engine = new PickFiveEngine();
        var result = engine.Result(quickPick);

        if (result.IsWin)
        {
            // Rememeber to implement winstreak logic once winstreak is implemented!!!
            player.WalletBalance += result.Payout; 
            
            //Log transaction after winning
            _db.WalletTransactions.Add(new WalletTransaction
            {
                PlayerId = Id,
                Type = TransactionType.Credit,
                Amount = result.Payout,
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
        
        TempData["QuickPick"] = quickPick;
        TempData["Win"] = result.IsWin;
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Info()
    {
        return View();
    }
    
    
}