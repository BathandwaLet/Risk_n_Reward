using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Web.Data;
using Risk_n_Reward.Core.Models.CrashModels;
using Risk_n_Reward.Core.Engines;

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
}