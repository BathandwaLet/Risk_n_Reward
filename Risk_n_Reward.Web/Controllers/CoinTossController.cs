using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Risk_n_Reward.Web.Models;
using Risk_n_Reward.Core;
using Risk_n_Reward.Web.Data;

namespace Risk_n_Reward.Web.Controllers;

public class CoinTossController : Controller
{
    private readonly ApplicationDbContext _context;
    private const int PlayerId = 1;
    private const int GameId = 3;
}