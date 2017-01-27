using System.Windows;

namespace DataBinding
{   
   public partial class VirtualizationTest
   {
      public VirtualizationTest()
      {
         InitializeComponent();
      }

      void OnWindowLoaded(object sender, RoutedEventArgs e)
      {
         for (var i = 0; i < 10000; i++)
         {
            fastCombobox.Items.Add(i.ToString());
            slowCombobox.Items.Add(i.ToString());
         }
      }
   }
}