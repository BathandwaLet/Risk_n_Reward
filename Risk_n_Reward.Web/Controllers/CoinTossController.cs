using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Core;
using Risk_n_Reward.Web.Data;

namespace Risk_n_Reward.Web.Controllers;

public class CoinTossController : Controller
{
    private readonly ApplicationDbContext _db;
    private const int PlayerId = 1;
    private const int GameId = 3;

    public CoinTossController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var player = await _db.Players.FindAsync(PlayerId);
        return View(player);
    }

    
}