/**
 * Создание WPF-приложений только кодом
 */

using System;
using System.Windows;

namespace CodeOnlyWpf
{
   class Program
   {
      [STAThread]
      static void Main()
      {
         Application application = new MainApplication();
      }
   }
}
