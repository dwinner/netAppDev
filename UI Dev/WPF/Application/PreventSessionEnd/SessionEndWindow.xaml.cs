using System;
using System.Windows;

namespace PreventSessionEnd
{   
   public partial class SessionEndWindow
   {
      public SessionEndWindow()
      {
         InitializeComponent();
      }

      private void OnExceptionClick(object sender, RoutedEventArgs e)
      {
         throw new ApplicationException("You clicked a bad button.");
      }
   }
}