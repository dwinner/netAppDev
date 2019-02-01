/**
 * Простое приложение Windows Forms
 */

using System.Windows.Forms;

namespace SimpleWinFormsApp
{
   internal static class Program
   {
      private const int DefaultWidth = 300;
      private const int DefaultHeight = 200;
      private const string Title = "My Window";

      private static void Main()
      {
         Application.Run(new MainWindow(Title, DefaultWidth, DefaultHeight));
      }
   }
}