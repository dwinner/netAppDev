using System.Linq;
using System.Windows;

namespace AttachedPropertyDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         MyAttachedPropertyProvider.SetMyProperty(FirstButton, 44);
         foreach (FrameworkElement item in LogicalTreeHelper.GetChildren(MainGrid).OfType<FrameworkElement>().Where(item => item != null))
         {
            SingleListBox.Items.Add(string.Format("{0}: {1}", item.Name, MyAttachedPropertyProvider.GetMyProperty(item)));
         }
      }
   }
}
