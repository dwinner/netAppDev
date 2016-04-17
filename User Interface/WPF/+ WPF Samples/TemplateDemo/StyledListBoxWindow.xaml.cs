using System.Windows;

namespace TemplateDemo
{
   /// <summary>
   /// Interaction logic for StyledListBoxWindow1.xaml
   /// </summary>
   public partial class StyledListBoxWindow : Window
   {
      public StyledListBoxWindow()
      {
         InitializeComponent();
         this.DataContext = Countries.GetCountries();
      }
   }
}
