namespace WpfApp
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      public int MyProperty { get; set; }

      protected override bool IsEnabledCore => base.IsEnabledCore;
   }
}