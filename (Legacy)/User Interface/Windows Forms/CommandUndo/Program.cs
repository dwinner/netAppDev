/**
 * Реализация отмены с помощью командных объектов
 */

using System;
using System.Windows.Forms;

namespace CommandUndo
{
   static class Program
   {
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new UndoForm());
      }
   }
}
