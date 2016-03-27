using System.Deployment.Application;
using System.Windows;

namespace ClickOnceWpfApp
{
   /// <summary>
   /// Пример развертывания при помощи технологии ClickOnce
   /// </summary>
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void ExecuteButton_Click(object sender, RoutedEventArgs e)
      {
         int first, second;
         ResultLabel.Content = int.TryParse(FirstTextBox.Text, out first) &&
                               int.TryParse(SecondTextBox.Text, out second)
            ? (object) (first + second)
            : "Input Error";
      }

      private void CheckForUpdatesButton_Click(object sender, RoutedEventArgs e)
      {
         if (!ApplicationDeployment.IsNetworkDeployed)
         {
            return;
         }

         ApplicationDeployment.CurrentDeployment.CheckForUpdateCompleted += (updater, args) =>
         {
            if (args.UpdateAvailable)
            {
               ApplicationDeployment.CurrentDeployment.UpdateCompleted +=
                  (completer, eventArgs) => MessageBox.Show("Update completed");
               ApplicationDeployment.CurrentDeployment.UpdateAsync();
            }
            else
            {
               MessageBox.Show("No update available");
            }
         };

         ApplicationDeployment.CurrentDeployment.CheckForUpdateAsync();
      }
   }
}
