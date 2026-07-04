using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Risk_n_Reward.Web.Models;

namespace Risk_n_Reward.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Attribute()
    {
        return View();
    }
}