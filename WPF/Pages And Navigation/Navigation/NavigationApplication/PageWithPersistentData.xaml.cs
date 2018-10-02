using System.Windows;

namespace NavigationApplication
{
   public partial class PageWithPersistentData
   {
      // ReSharper disable InconsistentNaming
      private static readonly DependencyProperty MyPageDataProperty;
      // ReSharper restore InconsistentNaming

      static PageWithPersistentData()
      {
         var metadata = new FrameworkPropertyMetadata { Journal = true };

         MyPageDataProperty = DependencyProperty.Register(
            "MyPageDataProperty", typeof(string), typeof(PageWithPersistentData),
            metadata, null);
      }

      public PageWithPersistentData()
      {
         InitializeComponent();
      }

      private string MyPageData
      {
         set { SetValue(MyPageDataProperty, value); }
         get { return (string)GetValue(MyPageDataProperty); }
      }

      private void SetText(object sender, RoutedEventArgs e)
      {
         MyPageData = Txt.Text;
         Txt.Text = string.Empty;
      }

      private void GetText(object sender, RoutedEventArgs e)
      {
         Lbl.Content = string.Format("Retrieved: {0}", MyPageData);
      }
   }
}