using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Web.Data;
using Risk_n_Reward.Core.Engines.HighLowEngine;

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
}