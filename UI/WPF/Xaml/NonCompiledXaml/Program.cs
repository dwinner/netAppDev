/**
 * Код и не компилированный XAML
 */

using System;
using System.Windows;

namespace NonCompiledXaml
{
   public class Program : Application
   {
      [STAThread]
      static void Main()
      {         
         var app = new Program { ShutdownMode = ShutdownMode.OnLastWindowClose };
         
         var window1 = new Window1("Window1.xaml");
         window1.Show();
         
         Xaml2009Window window2 = Xaml2009Window.LoadWindowFromXaml("Xaml2009.xaml");
         window2.Show();

         app.Run();
      }
   }
}
