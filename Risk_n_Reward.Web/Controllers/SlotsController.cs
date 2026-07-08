using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Web.Data;
using Risk_n_Reward.Core.Engines.SlotsEngine;
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
            return Redirect("Index");
        }

        if (player.WalletBalance < betAmount)
        {
            return Redirect("Index");
        }

        player.WalletBalance -= betAmount;
    }
}