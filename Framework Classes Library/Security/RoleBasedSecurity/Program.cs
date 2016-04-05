/**
 * Декларативная безопасность на основе ролей
 */

using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace RoleBasedSecurity
{
   class Program
   {
      static void Main()
      {
         AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

         try
         {
            ShowMessage();
         }
         catch (SecurityException securityEx)
         {
            // Перехвачено исключение безопасности.
            // Текущий принципал должен быть в локальной группе Users.
            Console.WriteLine("Security exception caught ({0})", securityEx.Message);
            Console.WriteLine("The current principal must be in the local Users group");
         }
      }

      [PrincipalPermission(SecurityAction.Demand, Role = "BUILTIN\\Users")]
      private static void ShowMessage()
      {
         // Текущий принципал вошел локально (член локальной группы Users)
         Console.WriteLine("The current principal is logged in locally ");
         Console.WriteLine("(member of the local Users group)");
      }
   }
}
