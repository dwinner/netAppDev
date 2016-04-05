/**
 * Создание и загрузка сборок динамическим образом
 */

using System.Windows;
using System.Windows.Media;

namespace DynamicAssembly
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void CompileAndRunButton_OnClick(object sender, RoutedEventArgs e)
      {
         TextOutputBlock.Background = Brushes.White;
         var driver = new CodeDriverInAppDomain();//new CodeDriver();
         bool isError;
         TextOutputBlock.Text = driver.CompileAndRun(TextCodeTextBox.Text, out isError);
         if (isError)
         {
            TextOutputBlock.Background = Brushes.Red;
         }
      }
   }
}
