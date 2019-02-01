using System.Windows.Controls;
using Formula1Demo.UserControls;

namespace Formula1Demo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         if (UiCtrlContainer == null)
            return;

         UiCtrlContainer.Child = null;
         var selectedIndex = (sender as ComboBox)?.SelectedIndex;
         if (selectedIndex == null)
            return;

         switch (selectedIndex.Value)
         {
            case 1:
               UiCtrlContainer.Child = new F1TreeUserControl();
               break;
            case 2:
               UiCtrlContainer.Child = new F1GridUserControl();
               break;
            case 3:
               UiCtrlContainer.Child = new F1CustomGridUserControl();
               break;
            case 4:
               UiCtrlContainer.Child = new F1GridGroupingUserControl();
               break;
         }
      }
   }
}