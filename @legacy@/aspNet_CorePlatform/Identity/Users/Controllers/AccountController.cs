﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
   [Authorize]
   public class AccountController : Controller
   {
      private AppUserManager UserManager
      {
         get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
      }

      private IAuthenticationManager AuthManager
      {
         get { return HttpContext.GetOwinContext().Authentication; }
      }

      [AllowAnonymous]
      public ActionResult Login(string returnUrl)
      {
         if (HttpContext.User.Identity.IsAuthenticated)
         {
            return View("Error", new[] { "Access Denied" });
         }

         ViewBag.ReturnUrl = returnUrl;
         return View();
      }

      [HttpPost]
      [AllowAnonymous]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Login(LoginModel details, string returnUrl)
      {
         if (ModelState.IsValid)
         {
            var user = await UserManager.FindAsync(details.Name, details.Password);
            if (user == null)
            {
               ModelState.AddModelError(string.Empty, "Invalid name or password");
            }
            else
            {
               var ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
               ident.AddClaims(LocationClaimsProvider.GetClaims(ident));
               ident.AddClaims(ClaimsRoles.CreateRolesFromClaims(ident));
               AuthManager.SignOut();
               AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
               return Redirect(returnUrl);
            }
         }

         ViewBag.ReturnUrl = returnUrl;
         return View(details);
      }

      [Authorize]
      public ActionResult Logout()
      {
         AuthManager.SignOut();
         return RedirectToAction("Index", "Home");
      }

      [HttpPost]
      [AllowAnonymous]
      public ActionResult GoogleLogin(string returnUrl)
      {
         var properties = new AuthenticationProperties
         {
            RedirectUri = Url.Action("GoogleLoginCallback", new { returnUrl })
         };
         HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
         return new HttpUnauthorizedResult();
      }

      [AllowAnonymous]
      public async Task<ActionResult> GoogleLoginCallback(string returnUrl)
      {
         var loginInfo = await AuthManager.GetExternalLoginInfoAsync();
         var user = await UserManager.FindAsync(loginInfo.Login);
         if (user == null)
         {
            user = new AppUser
            {
               Email = loginInfo.Email,
               UserName = loginInfo.DefaultUserName,
               City = Cities.London,
               Country = Countries.Uk
            };
            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
               return View("Error", result.Errors);
            }
            result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
            if (!result.Succeeded)
            {
               return View("Error", result.Errors);
            }
         }

         var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
         identity.AddClaims(loginInfo.ExternalIdentity.Claims);
         AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

         return Redirect(returnUrl ?? "/");
      }
   }
}