using System.Web.Mvc;
using System.Web.Profile;
using Gallery.Web.Models;

namespace Gallery.Web.Controllers
{
   /// <summary>
   /// Контроллер действий с профилем пользователя
   /// </summary>
   [Authorize]
   public class UserProfileController : Controller
   {
      /// <summary>
      /// Профиль пользователя
      /// </summary>
      /// <returns>Результат действия</returns>
      public ActionResult Index()
      {
         var profileWrapper = new UserProfileWrapper(ProfileBase.Create(User.Identity.Name));
         return View(profileWrapper);
      }

      /// <summary>
      /// Редактирование профиля пользователя
      /// </summary>
      /// <returns>Результат действия</returns>
      public ActionResult Edit()
      {
         var wrappedProfile = new UserProfileWrapper(ProfileBase.Create(User.Identity.Name));
         var userProfile = new UserProfileModel
         {
            FirstName = wrappedProfile.FirstName,
            PatronymicName = wrappedProfile.PatronymicName,
            LastName = wrappedProfile.LastName,
            PageSize = wrappedProfile.PageSize,
            MaxPictureMeasurement = wrappedProfile.MaxPictureMeasurement
         };

         return View(userProfile);
      }

      /// <summary>
      /// Редактирование профиля пользователя
      /// </summary>
      /// <param name="userProfile">Профиль пользователя</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      public ActionResult Edit(UserProfileModel userProfile)
      {
         if (ModelState.IsValid)
         {
            var wrappedProfile = new UserProfileWrapper(ProfileBase.Create(User.Identity.Name))
            {
               FirstName = userProfile.FirstName,
               PatronymicName = userProfile.PatronymicName,
               LastName = userProfile.LastName,
               PageSize = userProfile.PageSize,
               MaxPictureMeasurement = userProfile.MaxPictureMeasurement
            };
            wrappedProfile.Save();

            return RedirectToAction("Index");
         }

         return View(userProfile);
      }
   }
}
