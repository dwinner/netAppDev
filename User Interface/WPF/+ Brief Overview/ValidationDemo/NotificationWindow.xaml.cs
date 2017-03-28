namespace ValidationDemo
{
   public partial class NotificationWindow
   {
      private readonly SomeDataWithNotifications _data = new SomeDataWithNotifications();

      public NotificationWindow()
      {
         InitializeComponent();
         DataContext = _data;
      }
   }
}