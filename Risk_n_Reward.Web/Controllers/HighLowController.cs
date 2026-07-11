using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Web.Data;
using Risk_n_Reward.Core.Engines.HighLowEngine;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models.HighLowModels.BetTypes;
using Risk_n_Reward.Core.Models.HighLowModels.Outcomes;

namespace Risk_n_Reward.Web.Controllers;

public class HighLowController : Controller
{
    private ApplicationDbContext _db;
    private const int Id = 1;
    private const int GameId = 5;
    
    public HighLowController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<IActionResult> Index()
    {
        var player = await _db.Players.FindAsync(Id);
        return View(player);
    }

    [HttpPost]
    public async Task<IActionResult> Play(decimal betAmount, string playerSelection)
    {
        var player = await _db.Players.FindAsync(Id);

        if (player.Id == null)
        {
            return NotFound();
        }

        if (betAmount < 10.00m)
        {
            return RedirectToAction(nameof(Index));
        }
        
        if (player.WalletBalance < betAmount)
        {
            return RedirectToAction(nameof(Index));
        }

        player.WalletBalance -= betAmount;
        
        //Log 
        _db.WalletTransactions.Add(new WalletTransaction
        {
            PlayerId = Id,
            Type = TransactionType.Debit, //Placed bet
            Amount = betAmount,
            BalanceAfter = player.WalletBalance,
            CreatedAt = DateTime.UtcNow,
        });
        
        Deck deck = new Deck();
        Card firstCard = deck.Draw();
        Card nextCard = deck.Draw();
        
        var playerChoice = (playerSelection == "High") ? HL.Higher : (playerSelection == "Lower")? HL.Lower;
        var engine = new HighLowEngine();
        var result = engine.Result(firstCard, nextCard, playerChoice);

        var win = result.Outcome == HighLowOutcome.Win? true : false;
        decimal payout = 0;
        
        if (win)
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
        
        _db.GameSessions.Add(new GameSession
        {
            PlayerId = Id,
            BetAmount = betAmount,
            GameId = GameId,
            Outcome = (win)? Outcome.Win: Outcome.Loss,
            PlayedAt = DateTime.UtcNow,
        });
        
        await _db.SaveChangesAsync();
        
        TempData["Win"] = win;
        TempData["Payout"] = payout;
        
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Info()
    {
        return View();
    }
}