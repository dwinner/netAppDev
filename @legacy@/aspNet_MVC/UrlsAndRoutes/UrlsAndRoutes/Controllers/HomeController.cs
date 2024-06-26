﻿using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         ViewBag.Controller = "Home";
         ViewBag.Action = "Index";
         return View("ActionName");
      }

      public ActionResult CustomVariable(string id = "DefaultId")
      {
         ViewBag.Controller = "Home";
         ViewBag.Action = "CustomVariable";

         // Доступ к специальной переменной сегмента
         ViewBag.CustomVariable = id/* ?? "<no value>"*/;  // RouteData.Values["id"];
         return View();
      }
   }
}