/**
 * Локализация приложений Windows Forms
 */

using System;
using System.Windows.Forms;

namespace WinFormLocalization
{
   static class Program
   {      
      [STAThread]
      static void Main(string[] args)
      {
         string culture = string.Empty;
         if (args.Length == 1)
         {
            culture = args[0];
         }

         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new BookOfTheDayForm(culture));
      }
   }
}
