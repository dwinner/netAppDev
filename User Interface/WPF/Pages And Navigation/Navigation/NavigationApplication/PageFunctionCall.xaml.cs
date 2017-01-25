using System.Windows;

namespace NavigationApplication
{
   public partial class PageFunctionCall
   {
      public PageFunctionCall()
      {
         InitializeComponent();
      }

      private void OnSelectProduct(object sender, RoutedEventArgs e)
      {
         var pageFunction = new SelectProductPageFunction();
         pageFunction.Return += (o, args) =>
         {
            if (args != null)
               StatusLabel.Content = string.Format("You chose: {0}", args.Result.Name);
         };

         if (NavigationService != null)
            NavigationService.Navigate(pageFunction);
      }
   }
}