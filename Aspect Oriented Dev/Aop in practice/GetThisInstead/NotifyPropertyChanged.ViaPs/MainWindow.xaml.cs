using NotifyPropertyChanged.ViaPs.ViewModels;

namespace NotifyPropertyChanged.ViaPs
{   
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         DataContext = new NewNameViewModel();
      }
   }
}