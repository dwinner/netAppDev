using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Gallery.DataLevel.Orm.Extensions;
using Gallery.Web.Properties;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Gallery.Web.Filters;
using Gallery.Web.Models;

namespace Gallery.Web.Controllers
{
   /// <summary>
   /// Контроллер для действий с учетными записями
   /// </summary>
   [Authorize]
   [InitializeSimpleMembership]
   public class AccountController : Controller
   {
      /// <summary>
      /// Событие при входе пользователя
      /// </summary>
      public event EventHandler<LoginEventArgs> UserLoggedIn;      

      /// <summary>
      /// Безопасный вызов события входа
      /// </summary>
      /// <param name="e">Аргументы события входа</param>
      protected virtual void OnUserLoggedIn(LoginEventArgs e)
      {
         var userLoggedIn = UserLoggedIn;
         if (userLoggedIn != null)
         {
            userLoggedIn(this, e);
         }
      }

      /// <summary>
      /// Конструктор контроллера действий с учетными записями
      /// </summary>
      public AccountController()
      {
         UserLoggedIn += (sender, args) =>
         {
            var loggedInUser = args.UserName;
            AccountDbUtils.InsertIfNotExists(loggedInUser);
         };         
      }

      /// <summary>
      /// Вход: GET: /Account/Login
      /// </summary>
      /// <param name="returnUrl">Url возврата</param>
      /// <returns>Результат действия</returns>
      [AllowAnonymous]
      public ActionResult Login(string returnUrl)
      {
         ViewBag.ReturnUrl = returnUrl;
         return View();
      }

      /// <summary>
      /// Вход: POST: /Account/Login
      /// </summary>
      /// <param name="model">Модель входа</param>
      /// <param name="returnUrl">Url возврата</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      [AllowAnonymous]
      [ValidateAntiForgeryToken]
      public ActionResult Login(LoginModel model, string returnUrl)
      {
         if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.RememberMe))
         {
            OnUserLoggedIn(new LoginEventArgs(model.UserName));
            return RedirectToLocal(returnUrl);
         }

         // Появление этого сообщения означает наличие ошибки; повторное отображение формы
         ModelState.AddModelError("", Resources.UserNameOrPasswordInvalid);
         return View(model);
      }

      /// <summary>
      /// Выход: POST: /Account/LogOff
      /// </summary>
      /// <returns>Результат действия</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult LogOff()
      {
         WebSecurity.Logout();
         return RedirectToAction("Login", "Account");
      }

      /// <summary>
      /// Регистрация нового пользователя: GET: /Account/Register
      /// </summary>
      /// <returns>Результат действия</returns>
      [AllowAnonymous]
      public ActionResult Register()
      {
         return View();
      }

      /// <summary>
      /// Регистрация нового пользователя: POST: /Account/Register
      /// </summary>
      /// <param name="model">Модель регистрации</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      [AllowAnonymous]
      [ValidateAntiForgeryToken]
      public ActionResult Register(RegisterModel model)
      {
         if (ModelState.IsValid)
         {
            // Попытка зарегистрировать пользователя
            try
            {
               WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
               WebSecurity.Login(model.UserName, model.Password);
               OnUserLoggedIn(new LoginEventArgs(model.UserName));
               var wrappedProfile = new UserProfileWrapper(ProfileBase.Create(model.UserName))
               {
                  FirstName = model.FirstName,
                  PatronymicName = model.PatronymicName,
                  LastName = model.LastName
               };
               wrappedProfile.Save();

               return RedirectToAction("Edit", "UserProfile");
            }
            catch (MembershipCreateUserException e)
            {
               ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
            }
         }

         // Появление этого сообщения означает наличие ошибки; повторное отображение формы
         return View(model);
      }

      /// <summary>
      /// Удаление учетной записи конкретного провайдера :POST: /Account/Disassociate 
      /// </summary>
      /// <param name="provider">Имя провайдера</param>
      /// <param name="providerUserId">UserId, связанное с провайдером</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Disassociate(string provider, string providerUserId)
      {
         string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
         ManageMessageId? message = null;

         // Удалять связь учетной записи, только если текущий пользователь — ее владелец
         if (ownerAccount == User.Identity.Name)
         {
            // Транзакция используется, чтобы помешать пользователю удалить учетные данные последнего входа
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
               bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
               if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
               {
                  OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                  scope.Complete();
                  message = ManageMessageId.RemoveLoginSuccess;
               }
            }
         }

         return RedirectToAction("Manage", new { Message = message });
      }

      /// <summary>
      /// Действие с учетной записью: GET: /Account/Manage
      /// </summary>
      /// <param name="message">Состояние выполнение действий с учетной записью</param>
      /// <returns>Результат действия</returns>
      public ActionResult Manage(ManageMessageId? message)
      {
         ViewBag.StatusMessage =
             message == ManageMessageId.ChangePasswordSuccess ? Resources.PasswordHasChanged
             : message == ManageMessageId.SetPasswordSuccess ? Resources.PasswordIsSet
             : message == ManageMessageId.RemoveLoginSuccess ? Resources.ExternalAccountHasDeleted
             : "";
         ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
         ViewBag.ReturnUrl = Url.Action("Manage");
         return View();
      }

      /// <summary>
      /// Управление паролями: POST: /Account/Manage 
      /// </summary>
      /// <param name="model">Модель для управления паролями</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Manage(LocalPasswordModel model)
      {
         bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
         ViewBag.HasLocalPassword = hasLocalAccount;
         ViewBag.ReturnUrl = Url.Action("Manage");
         if (hasLocalAccount)
         {
            if (ModelState.IsValid)
            {
               // В ряде случаев при сбое ChangePassword породит исключение, а не вернет false.
               bool changePasswordSucceeded;
               try
               {
                  changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
               }
               catch (Exception)
               {
                  changePasswordSucceeded = false;
               }

               if (changePasswordSucceeded)
               {
                  return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
               }
               ModelState.AddModelError("", Resources.WrongCurrentOrNewPassword);
            }
         }
         else
         {
            // У пользователя нет локального пароля, уберите все ошибки проверки, вызванные отсутствующим
            // полем OldPassword
            ModelState state = ModelState["OldPassword"];
            if (state != null)
            {
               state.Errors.Clear();
            }

            if (ModelState.IsValid)
            {
               try
               {
                  WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                  return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
               }
               catch (Exception)
               {
                  ModelState.AddModelError("",
                     String.Format(
                        Resources.FailToCreateAccount,
                        User.Identity.Name));
               }
            }
         }

         // Появление этого сообщения означает наличие ошибки; повторное отображение формы
         return View(model);
      }
      
      /// <summary>
      /// Вход через внешнюю службу :POST: /Account/ExternalLogin 
      /// </summary>
      /// <param name="provider">Имя внешнего провайдера</param>
      /// <param name="returnUrl">Url возврата</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      [AllowAnonymous]
      [ValidateAntiForgeryToken]
      public ActionResult ExternalLogin(string provider, string returnUrl)
      {
         return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
      }
      
      /// <summary>
      /// Обратный вызов при внешнем входе: GET: /Account/ExternalLoginCallback 
      /// </summary>
      /// <param name="returnUrl">Url возврата</param>
      /// <returns>Результат действия</returns>
      [AllowAnonymous]
      public ActionResult ExternalLoginCallback(string returnUrl)
      {
         AuthenticationResult result =
            OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new {ReturnUrl = returnUrl}));
         if (!result.IsSuccessful)
         {
            return RedirectToAction("ExternalLoginFailure");
         }

         if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, false))
         {
            return RedirectToLocal(returnUrl);
         }

         if (User.Identity.IsAuthenticated)
         {
            // Если текущий пользователь вошел в систему, добавляется новая учетная запись
            OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
            return RedirectToLocal(returnUrl);
         }

         // Новый пользователь, запрашиваем желаемое имя участника
         string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
         ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
         ViewBag.ReturnUrl = returnUrl;
         return View("ExternalLoginConfirmation",
            new RegisterExternalLoginModel {UserName = result.UserName, ExternalLoginData = loginData});
      }
      
      /// <summary>
      /// Подтверждение при входе через внешние службы: POST: /Account/ExternalLoginConfirmation
      /// </summary>
      /// <param name="model">Модель для внешней регистрации</param>
      /// <param name="returnUrl">Url возврата</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      [AllowAnonymous]
      [ValidateAntiForgeryToken]
      public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
      {
         string provider;
         string providerUserId;

         if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
         {
            return RedirectToAction("Manage");
         }

         if (ModelState.IsValid)
         {
            // Добавление нового пользователя в базу данных
            using (var db = new UsersContext())
            {
               UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
               // Проверка наличия пользователя в базе данных
               if (user == null)
               {
                  // Добавление имени в таблицу профиля
                  db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                  db.SaveChanges();

                  OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                  OAuthWebSecurity.Login(provider, providerUserId, false);

                  return RedirectToLocal(returnUrl);
               }

               ModelState.AddModelError("UserName", Resources.UserNameAlreadyExists);
            }
         }

         ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
         ViewBag.ReturnUrl = returnUrl;
         return View(model);
      }

      /// <summary>
      /// Неудачное действие при входе через внешние службы
      /// </summary>
      /// <returns>Результат действия</returns>
      [AllowAnonymous]
      public ActionResult ExternalLoginFailure()
      {
         return View();
      }

      /// <summary>
      /// Действие для списка аккаунтов внешних слубж
      /// </summary>
      /// <param name="returnUrl">Url возврата</param>
      /// <returns>Результат действия</returns>
      [AllowAnonymous]
      [ChildActionOnly]
      public ActionResult ExternalLoginsList(string returnUrl)
      {
         ViewBag.ReturnUrl = returnUrl;
         return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
      }

      /// <summary>
      /// Действие на удаление аккаунтов внешних слубж
      /// </summary>
      /// <returns>Результат действия</returns>
      [ChildActionOnly]
      public ActionResult RemoveExternalLogins()
      {
         ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
         var externalLogins = (from account in accounts
                               let clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider)
                               select new ExternalLogin
                               {
                                  Provider = account.Provider,
                                  ProviderDisplayName = clientData.DisplayName,
                                  ProviderUserId = account.ProviderUserId,
                               }).ToList();

         ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
         return PartialView("_RemoveExternalLoginsPartial", externalLogins);
      }

      #region Вспомогательные методы

      /// <summary>
      /// Действие при внутреннем перенаправлении
      /// </summary>
      /// <param name="returnUrl">Url-возврата</param>
      /// <returns>Результат действия</returns>
      private ActionResult RedirectToLocal(string returnUrl)
      {
         if (Url.IsLocalUrl(returnUrl))
         {
            return Redirect(returnUrl);
         }

         return RedirectToAction("Edit", "UserProfile");
      }

      /// <summary>
      /// Состояния действий выполнения операций с учетной записью
      /// </summary>
      public enum ManageMessageId
      {
         /// <summary>
         /// Успешная смена пароля
         /// </summary>
         ChangePasswordSuccess,

         /// <summary>
         /// Успешная установка пароля
         /// </summary>
         SetPasswordSuccess,

         /// <summary>
         /// Успешное удаление логина
         /// </summary>
         RemoveLoginSuccess,
      }

      /// <summary>
      /// Действие при входе через внешние службы
      /// </summary>
      internal class ExternalLoginResult : ActionResult
      {
         /// <summary>
         /// Конструктор действия при входе через внешние службы
         /// </summary>
         /// <param name="provider">Строка с поставщиком входа</param>
         /// <param name="returnUrl">Url возврата</param>
         public ExternalLoginResult(string provider, string returnUrl)
         {
            Provider = provider;
            ReturnUrl = returnUrl;
         }

         /// <summary>
         /// Строка с поставщиком входа
         /// </summary>
         public string Provider { get; private set; }

         /// <summary>
         /// Url возврата
         /// </summary>
         public string ReturnUrl { get; private set; }

         /// <summary>
         /// Обработка результата выполнения метода действия
         /// </summary>
         /// <param name="context">Контекст запроса</param>
         public override void ExecuteResult(ControllerContext context)
         {
            OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);            
         }
      }

      /// <summary>
      /// Преобразование кода ошибки, которая может возникнуть при создании пользователя, к строке
      /// </summary>
      /// <param name="createStatus">Статус создания</param>
      /// <returns>Строка статуса</returns>
      private static string ErrorCodeToString(MembershipCreateStatus createStatus)
      {
         switch (createStatus)
         {
            case MembershipCreateStatus.DuplicateUserName:
               return Resources.UserNameAlreadyExists;

            case MembershipCreateStatus.DuplicateEmail:
               return Resources.DuplicateEmail;

            case MembershipCreateStatus.InvalidPassword:
               return Resources.InvalidPassword;

            case MembershipCreateStatus.InvalidEmail:
               return Resources.InvalidEmail;

            case MembershipCreateStatus.InvalidAnswer:
               return Resources.InvalidAnswer;

            case MembershipCreateStatus.InvalidQuestion:
               return Resources.InvalidQuestion;

            case MembershipCreateStatus.InvalidUserName:
               return Resources.InvalidUserName;

            case MembershipCreateStatus.ProviderError:
               return Resources.ProviderError;

            case MembershipCreateStatus.UserRejected:
               return Resources.UserRejected;

            default:
               return Resources.UnknownError;
         }
      }

      #endregion
   }
}
