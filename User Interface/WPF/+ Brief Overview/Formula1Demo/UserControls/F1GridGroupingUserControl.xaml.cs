using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace Formula1Demo.UserControls
{
   public partial class F1GridGroupingUserControl
   {
      private readonly ObjectDataProvider _racesOdp;

      public F1GridGroupingUserControl()
      {
         InitializeComponent();
         _racesOdp = TryFindResource("Races") as ObjectDataProvider;
         Debug.Assert(_racesOdp != null);
      }

      private void OnGetPage(object sender, RoutedEventArgs e)
      {
         var page = int.Parse(PageNumberTextBox.Text);
         _racesOdp.MethodParameters[0] = page;
         _racesOdp.Refresh();
      }
   }
}