﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using SecureWebAPI.Models;
using SecureWebAPI.Providers;
using SecureWebAPI.Results;

namespace SecureWebAPI.Controllers
{
   [Authorize]
   [RoutePrefix("api/Account")]
   public class AccountController : ApiController
   {
      private const string LocalLoginProvider = "Локально";

      public AccountController()
         : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
      {
      }

      public AccountController(UserManager<IdentityUser> userManager,
         ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
      {
         UserManager = userManager;
         AccessTokenFormat = accessTokenFormat;
      }

      public UserManager<IdentityUser> UserManager { get; private set; }
      public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

      // GET api/Account/UserInfo
      [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
      [Route("UserInfo")]
      public UserInfoViewModel GetUserInfo()
      {
         ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

         return new UserInfoViewModel
         {
            UserName = User.Identity.GetUserName(),
            HasRegistered = externalLogin == null,
            LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
         };
      }

      // POST api/Учетная запись/Выход
      [Route("Logout")]
      public IHttpActionResult Logout()
      {
         Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
         return Ok();
      }

      // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
      [Route("ManageInfo")]
      public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
      {
         IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

         if (user == null)
         {
            return null;
         }

         var logins = new List<UserLoginInfoViewModel>();

         foreach (IdentityUserLogin linkedAccount in user.Logins)
         {
            logins.Add(new UserLoginInfoViewModel
            {
               LoginProvider = linkedAccount.LoginProvider,
               ProviderKey = linkedAccount.ProviderKey
            });
         }

         if (user.PasswordHash != null)
         {
            logins.Add(new UserLoginInfoViewModel
            {
               LoginProvider = LocalLoginProvider,
               ProviderKey = user.UserName,
            });
         }

         return new ManageInfoViewModel
         {
            LocalLoginProvider = LocalLoginProvider,
            UserName = user.UserName,
            Logins = logins,
            ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
         };
      }

      // POST api/Account/ChangePassword
      [Route("ChangePassword")]
      public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
            model.NewPassword);
         IHttpActionResult errorResult = GetErrorResult(result);

         if (errorResult != null)
         {
            return errorResult;
         }

         return Ok();
      }

      // POST api/Account/SetPassword
      [Route("SetPassword")]
      public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
         IHttpActionResult errorResult = GetErrorResult(result);

         if (errorResult != null)
         {
            return errorResult;
         }

         return Ok();
      }

      // POST api/Account/AddExternalLogin
      [Route("AddExternalLogin")]
      public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

         AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

         if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                                                           && ticket.Properties.ExpiresUtc.HasValue
                                                           && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
         {
            return BadRequest("Сбой внешнего входа.");
         }

         ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

         if (externalData == null)
         {
            return BadRequest("Внешнее имя входа уже связано с учетной записью.");
         }

         IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
            new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

         IHttpActionResult errorResult = GetErrorResult(result);

         if (errorResult != null)
         {
            return errorResult;
         }

         return Ok();
      }

      // POST api/Account/RemoveLogin
      [Route("RemoveLogin")]
      public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         IdentityResult result;

         if (model.LoginProvider == LocalLoginProvider)
         {
            result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
         }
         else
         {
            result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
               new UserLoginInfo(model.LoginProvider, model.ProviderKey));
         }

         IHttpActionResult errorResult = GetErrorResult(result);

         if (errorResult != null)
         {
            return errorResult;
         }

         return Ok();
      }

      // GET api/Account/ExternalLogin
      [OverrideAuthentication]
      [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
      [AllowAnonymous]
      [Route("ExternalLogin", Name = "ExternalLogin")]
      public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
      {
         if (error != null)
         {
            return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
         }

         if (!User.Identity.IsAuthenticated)
         {
            return new ChallengeResult(provider, this);
         }

         ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

         if (externalLogin == null)
         {
            return InternalServerError();
         }

         if (externalLogin.LoginProvider != provider)
         {
            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            return new ChallengeResult(provider, this);
         }

         IdentityUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
            externalLogin.ProviderKey));

         bool hasRegistered = user != null;

         if (hasRegistered)
         {
            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity oAuthIdentity = await UserManager.CreateIdentityAsync(user,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookieIdentity = await UserManager.CreateIdentityAsync(user,
               CookieAuthenticationDefaults.AuthenticationType);
            AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
            Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
         }
         else
         {
            IEnumerable<Claim> claims = externalLogin.GetClaims();
            var identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
            Authentication.SignIn(identity);
         }

         return Ok();
      }

      // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
      [AllowAnonymous]
      [Route("ExternalLogins")]
      public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
      {
         IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();

         string state;

         if (generateState)
         {
            const int strengthInBits = 256;
            state = RandomOAuthStateGenerator.Generate(strengthInBits);
         }
         else
         {
            state = null;
         }

         return descriptions.Select(description => new ExternalLoginViewModel
         {
            Name = description.Caption,
            Url = Url.Route("ExternalLogin", new
            {
               provider = description.AuthenticationType,
               response_type = "token",
               client_id = Startup.PublicClientId,
               redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
               state
            }),
            State = state
         }).ToList();
      }

      // POST api/Account/Register
      [AllowAnonymous]
      [Route("Register")]
      public async Task<IHttpActionResult> Register(RegisterBindingModel model)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var user = new IdentityUser
         {
            UserName = model.UserName
         };

         IdentityResult result = await UserManager.CreateAsync(user, model.Password);
         IHttpActionResult errorResult = GetErrorResult(result);

         return errorResult ?? Ok();
      }

      // POST api/Account/RegisterExternal
      [OverrideAuthentication]
      [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
      [Route("RegisterExternal")]
      public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

         if (externalLogin == null)
         {
            return InternalServerError();
         }

         var user = new IdentityUser
         {
            UserName = model.UserName
         };
         user.Logins.Add(new IdentityUserLogin
         {
            LoginProvider = externalLogin.LoginProvider,
            ProviderKey = externalLogin.ProviderKey
         });
         IdentityResult result = await UserManager.CreateAsync(user);
         IHttpActionResult errorResult = GetErrorResult(result);

         if (errorResult != null)
         {
            return errorResult;
         }

         return Ok();
      }

      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            UserManager.Dispose();
         }

         base.Dispose(disposing);
      }

      #region Вспомогательные приложения

      private IAuthenticationManager Authentication
      {
         get { return Request.GetOwinContext().Authentication; }
      }

      private IHttpActionResult GetErrorResult(IdentityResult result)
      {
         if (result == null)
         {
            return InternalServerError();
         }

         if (result.Succeeded)
         {
            return null;
         }

         if (result.Errors == null)
         {
            return ModelState.IsValid ? (IHttpActionResult)BadRequest() : BadRequest(ModelState);
         }

         foreach (string error in result.Errors)
         {
            ModelState.AddModelError("", error);
         }

         // Ошибки ModelState для отправки отсутствуют, поэтому просто возвращается пустой BadRequest.
         return ModelState.IsValid ? (IHttpActionResult)BadRequest() : BadRequest(ModelState);
      }

      private class ExternalLoginData
      {
         public string LoginProvider { get; set; }
         public string ProviderKey { get; set; }
         public string UserName { get; set; }

         public IList<Claim> GetClaims()
         {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

            if (UserName != null)
            {
               claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
            }

            return claims;
         }

         public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
         {
            if (identity == null)
            {
               return null;
            }

            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                || String.IsNullOrEmpty(providerKeyClaim.Value))
            {
               return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
               return null;
            }

            return new ExternalLoginData
            {
               LoginProvider = providerKeyClaim.Issuer,
               ProviderKey = providerKeyClaim.Value,
               UserName = identity.FindFirstValue(ClaimTypes.Name)
            };
         }
      }

      private static class RandomOAuthStateGenerator
      {
         private static readonly RandomNumberGenerator Random = new RNGCryptoServiceProvider();

         public static string Generate(int strengthInBits)
         {
            const int bitsPerByte = 8;

            if (strengthInBits % bitsPerByte != 0)
            {
               throw new ArgumentException("Значение strengthInBits должно делиться на 8.", "strengthInBits");
            }

            int strengthInBytes = strengthInBits / bitsPerByte;

            var data = new byte[strengthInBytes];
            Random.GetBytes(data);
            return HttpServerUtility.UrlTokenEncode(data);
         }
      }

      #endregion
   }
}