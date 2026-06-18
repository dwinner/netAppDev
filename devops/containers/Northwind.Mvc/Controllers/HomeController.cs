using System.Diagnostics;
using EnvironmentLib;
using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;

namespace Northwind.Mvc.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
   private readonly ILogger<HomeController> _logger = logger;

   public IActionResult Index()
   {
      EnvironmentInfo model = new();
      return View(model);
   }

   public IActionResult Privacy() => View();

   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   public IActionResult Error() => View(new ErrorViewModel
      { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}