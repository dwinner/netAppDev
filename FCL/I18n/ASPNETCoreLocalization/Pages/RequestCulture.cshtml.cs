using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace ASPNETCoreLocalization.Pages
{
   public class RequestCultureModel : PageModel
   {
      public void OnGet()
      {
         var features = HttpContext.Features.ToList();
         var feature = HttpContext.Features.Get<IRequestCultureFeature>();
         RequestCulture requestCulture = feature.RequestCulture;
         RequestCulture = requestCulture.UICulture.ToString();
         Today = DateTime.Today.ToLongDateString();
      }

      public string? RequestCulture { get; private set; }
      public string? Today { get; private set; }
   }
}