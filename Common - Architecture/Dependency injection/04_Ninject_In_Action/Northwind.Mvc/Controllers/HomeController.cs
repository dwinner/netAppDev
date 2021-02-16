using System;
using System.Web.Mvc;
using Northwind.Core;

namespace Northwind.Mvc.Controllers
{
   public class HomeController : Controller
   {
      private readonly ICustomerRepository _repository;

      public HomeController(ICustomerRepository repository) =>
         _repository = repository ?? throw new ArgumentNullException(nameof(repository));

      [Log(LogLevel = "Info")]
      public ActionResult Index()
      {
         var customers = _repository.GetAll();
         return View(customers);
      }

      public ActionResult Create() => View();

      [HttpPost]
      public ActionResult Create(Customer customer)
      {
         if (ModelState.IsValid)
         {
            _repository.Add(customer);
            return RedirectToAction("Index");
         }

         return View();
      }

      public ActionResult About() => View();
   }
}