using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppWithADSample.Pages
{
   [Authorize]
   public class UserInfoModel : PageModel
   {
      public string? UserName { get; private set; }

      public List<(string Type, string Subject, string Value)> ClaimsInformation { get; } = new();

      public void OnGet()
      {
         UserName = User.Identity?.Name;

         foreach (var claim in User.Claims)
         {
            ClaimsInformation.Add((claim.Type, claim.Subject?.Name ?? string.Empty, claim.Value));
         }
      }
   }
}