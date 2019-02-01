using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Multithreading
{
   public partial class Window1
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void OnBreakRules(object sender, RoutedEventArgs e)
      {
         var thread = new Thread(UpdateTextWrong);
         thread.Start();
      }

      private void UpdateTextWrong()
      {
         TestTextBox.Text = "Here is some new text.";
      }

      private void OnFollowRules(object sender, RoutedEventArgs e)
      {
         var thread = new Thread(UpdateTextRight);
         thread.Start();
      }

      private void UpdateTextRight()
      {
         Thread.Sleep(TimeSpan.FromSeconds(5));
         Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            (ThreadStart) delegate { TestTextBox.Text = "Here is some new text."; }
         );
      }

      private void OnUseBackgroundWorker(object sender, RoutedEventArgs e)
      {
         var test = new BackgroundWorkerTest();
         test.ShowDialog();
      }
   }
}