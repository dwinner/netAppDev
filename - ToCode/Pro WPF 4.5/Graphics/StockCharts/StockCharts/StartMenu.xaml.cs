using System.Windows;

namespace StockCharts
{
   /// <summary>
   /// Interaction logic for StartMenu.xaml
   /// </summary>
   public partial class StartMenu : Window
   {
      public StartMenu()
      {
         InitializeComponent();
      }

      private void StaticStock_Click(object sender, RoutedEventArgs e)
      {
         StaticStockCharts ssc = new StaticStockCharts();
         ssc.ShowDialog();
      }

      private void Close_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }

      private void MovingAverage_Click(object sender, RoutedEventArgs e)
      {
         MovingAverage ma = new MovingAverage();
         ma.ShowDialog();
      }

      private void YahooStock_Click(object sender, RoutedEventArgs e)
      {
         YahooStockChart ysc = new YahooStockChart();
         ysc.ShowDialog();
      }
   }
}
