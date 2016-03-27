using System.Web.Mvc;

namespace WebApiServices.Controllers
{
   public class HomeController : Controller
   {
      //private readonly ReservationRepository _repository = ReservationRepository.Current;

      public ViewResult Index()
      {
         return View();
      }

      //public ActionResult Index()
      //{
      //   return View(_repository.GetAll());
      //}

      //public ActionResult Add(Reservation item)
      //{
      //   if (ModelState.IsValid)
      //   {
      //      _repository.Add(item);
      //      return RedirectToAction("Index");
      //   }

      //   return View("Index");
      //}

      //public ActionResult Remove(int id)
      //{
      //   _repository.Remove(id);
      //   return RedirectToAction("Index");
      //}

      //public ActionResult Update(Reservation item)
      //{
      //   return ModelState.IsValid && _repository.Update(item)
      //      ? (ActionResult) RedirectToAction("Index")
      //      : View("Index");
      //}
   }
}