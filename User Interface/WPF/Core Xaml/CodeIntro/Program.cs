/**
 * Отображение элементов на объекты .NET
 */

using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeIntro
{
   internal class Program
   {
      [STAThread]
      private static void Main()
      {
         var theButton = new Button
         {
            Content = "Click Me!"
         };

         var theWindow = new Window
         {
            Title = "Code Demo",
            Content = theButton
         };

         var application = new Application();
         application.Run(theWindow);
      }
   }
}