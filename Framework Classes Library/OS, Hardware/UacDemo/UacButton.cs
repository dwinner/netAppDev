using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace EventLogDemo
{
   /// <summary>
   /// Кнопка вызова компонента UAC
   /// </summary>
   public class UacButton : Button
   {
      private bool _showShield;

      public bool ShowShield
      {
         get
         {
            return _showShield;
         }
         set
         {
            bool needToShow = value && !IsElevated();
            // Передать с параметром lParam единицу в качестве значения true
            if (Handle != IntPtr.Zero)
            {
               Win32.SendMessage(new HandleRef(this, Handle), Win32.BcmSetshield, new IntPtr(0),
                  new IntPtr(needToShow ? 1 : 0));
               _showShield = needToShow;
            }
         }
      }

      private static bool IsElevated() // Если прав достаточно, нет необходимости в их повышении
      {
         var identity = WindowsIdentity.GetCurrent();
         if (identity == null)
         {
            return false;
         }

         var principal = new WindowsPrincipal(identity);
         return principal.IsInRole(WindowsBuiltInRole.Administrator);
      }

      public UacButton()
      {
         // NOTE: без этой строчки пиктограмма не появится
         FlatStyle = FlatStyle.System;
      }
   }
}
