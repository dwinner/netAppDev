using System.Windows;
using FirstsStepsRUI.Repositories.Concretes;
using FirstsStepsRUI.ViewModels;
using FirstsStepsRUI.Views;
using ReactiveUI;
using Splat;

namespace FirstsStepsRUI
{
   public partial class App
   {
      private static ShellView _shellView;

      public App()
      {
         var bootstrapper = new AppBootstrapper();
         bootstrapper.Locate();
      }

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         _shellView = (ShellView) Locator.Current.GetService<IViewFor<ShellViewModel>>();
         _shellView.Show();
      }
   }
}