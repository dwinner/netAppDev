namespace PopupPanelSample
{   
   public partial class MainWindow
   {
      public MainWindow( )
      {
         InitializeComponent();
         rootWindow.DataContext = new SampleViewModel();
      }
   }

   public class Address
   {
      public string Name
      {
         get { return "An Address"; }
      }

      public override string ToString( )
      {
         return Name;
      }
   }

   public class Phone
   {
      public string Name
      {
         get { return "A Phone"; }
      }

      public override string ToString( )
      {
         return Name;
      }
   }

   public class Email
   {
      public string Name
      {
         get { return "An Email"; }
      }

      public override string ToString( )
      {
         return Name;
      }
   }
}