using System.Windows;

namespace MVVMDemo
{   
   public partial class App
   {
      readonly MainWindowViewModel _mainViewModel = new MainWindowViewModel();

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         _mainViewModel.Close += (sender, args) => Shutdown();
         var window = new MainWindow();
         MainWindow.DataContext = _mainViewModel;
         window.Show();
      }
   }
}
