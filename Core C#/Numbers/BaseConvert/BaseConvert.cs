/**
 * Преобразование чисел в различные системы счисления
 */
using System;
using System.Windows.Forms;

namespace HowToCSharp.Ch05.BaseConvert
{
   public static class BaseConvert
   {
      [STAThread]
      public static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new BaseConvertForm());
      }
   }
}
