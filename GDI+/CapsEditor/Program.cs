/**
 * Пример редактирования и печати текста
 */

using System;
using System.Windows.Forms;

namespace CapsEditor
{
   internal static class Program
   {      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new CapsEditorForm());
      }
   }
}