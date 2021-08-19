namespace RibbonWin
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         var employeFrom = new EmployeeInfo();
         UiPanel.Children.Add(employeFrom);
      }
   }
}