/**
 * Обработка неперехваченных исключений в WPF
 */

using System;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace HowToCSharp.Ch04.UnhandledExceptionWPF
{
   /// <summary>
   /// Логика взаимодействия в окне Window1.xaml
   /// </summary>
   public partial class Window1
   {
      public Window1()
      {
         InitializeComponent();
         Application.Current.DispatcherUnhandledException += CurrentDispatcherUnhandledException;
      }

      static void CurrentDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
      {         
         var sb = new StringBuilder();
         sb.AppendLine("Caught unhandled exception");
         sb.AppendLine(e.Exception.ToString());

         MessageBox.Show(sb.ToString());

         e.Handled = true; // Предотвращаем выход, говоря, что обработка была
      }

      private void Button1Click(object sender, RoutedEventArgs e)
      {
         throw new InvalidOperationException("Oops");
      }
   }
}
