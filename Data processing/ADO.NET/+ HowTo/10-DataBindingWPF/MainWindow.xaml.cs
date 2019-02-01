using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace _10_DataBindingWPF
{
   /// <summary>
   /// Логика взаимодействия для MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private static readonly string BooksConnStr = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;

      public MainWindow()
      {
         InitializeComponent();
         DataSet dataSet = CreateDataSet();
         ListView.DataContext = dataSet.Tables["Books"];
         var binding = new Binding();
         ListView.SetBinding(ItemsControl.ItemsSourceProperty, binding);
      }

      private static DataSet CreateDataSet()
      {         
         using (var conn = new SqlConnection(BooksConnStr))
         {
            conn.Open();
            using (var cmd = new SqlCommand("SELECT * FROM Books", conn))
            using (var adapter = new SqlDataAdapter())
            {
               adapter.TableMappings.Add("Table", "Books");
               adapter.SelectCommand = cmd;
               var dataSet = new DataSet("Books");
               adapter.Fill(dataSet);  // Поместить все строки в набор данных
               return dataSet;
            }
         }
      }
   }
}
