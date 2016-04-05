/**
 * Создание задачи-марионетки
 */

using System;
using System.Windows.Forms;

namespace _07_PuppetTask
{
   static class Program
   {
      /// <summary>
      /// Главная точка входа для приложения.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new PuppetForm());
      }
   }
}
