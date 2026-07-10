using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Web.Data;
using Risk_n_Reward.Core.Engines.HighLowEngine;
using Risk_n_Reward.Core.Models.CardDeck;
using Risk_n_Reward.Core.Models.HighLowModels.BetTypes;

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

        Deck deck = new Deck();
        Card firstCard = deck.Draw();
        Card nextCard = deck.Draw();

        var playerChoice = (playerSelection == "High") ? HL.Higher : HL.Lower;
        
        var engine = new HighLowEngine();
        var result = engine.Result(firstCard, nextCard, playerChoice);
        
        
    }
    public async Task<IActionResult> Info()
    {
        return View();
    }
}