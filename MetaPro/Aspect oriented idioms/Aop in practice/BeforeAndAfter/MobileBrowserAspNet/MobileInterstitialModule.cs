using System;
using System.Web;

namespace MobileBrowserAspNet
{
   public class MobileInterstitialModule : IHttpModule
   {
      public void Init(HttpApplication context)
      {
         context.BeginRequest += context_BeginRequest;
      }

      public void Dispose()
      {
      }

      private void context_BeginRequest(object sender, EventArgs e)
      {
         var httpContext = HttpContext.Current;
         if (!httpContext.Request.Url.AbsolutePath.Contains(".aspx"))
            return;
         if (IsNoThanksCookieSet())
            return;
         if (OnMobileInterstitial())
            return;
         if (ComingFromMobileInterstitial())
            return;

         var mobileDetect = new MobileDetect(httpContext);
         if (mobileDetect.IsMobile())
         {
            var url = httpContext.Request.RawUrl;
            var encodedUrl = HttpUtility.UrlEncode(url);
            httpContext.Response.Redirect("MobileInterstitial.aspx?returnUrl=" + encodedUrl);
         }
      }

      private bool IsNoThanksCookieSet()
      {
         return HttpContext.Current.Request.Cookies["NoThanks"] != null;
      }

      private bool ComingFromMobileInterstitial()
      {
         var httpRequest = HttpContext.Current.Request;
         if (httpRequest.UrlReferrer == null)
            return false;
         return httpRequest.UrlReferrer.AbsoluteUri.Contains("MobileInterstitial.aspx");
      }

      private bool OnMobileInterstitial()
      {
         var httpRequest = HttpContext.Current.Request;
         return httpRequest.RawUrl.Contains("MobileInterstitial.aspx");
      }
   }
}