/**
 * Взаимные преобразования градусов в радианы
 */
using System;
using System.Windows.Forms;

namespace HowToCSharp.Ch05.RadiansAndDegrees
{
   static class RadiansAndDegrees
   {
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new RadiansAndDegreesForm());
      }
   }
}
