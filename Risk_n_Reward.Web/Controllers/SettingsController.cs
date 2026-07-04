using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;

namespace Risk_n_Reward.Web.Controllers;

public class SettingsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult PlayerHub()
    {
        return View();
    }
}