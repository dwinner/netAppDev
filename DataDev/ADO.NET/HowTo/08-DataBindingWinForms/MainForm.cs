using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataBindingWinForms
{
   public partial class MainForm : Form
   {
      private const string SELECT_CMD = "SELECT * FROM Books";
      private readonly DataSet _dataSet;
      private static readonly string BooksConStr = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;

      public MainForm()
      {
         InitializeComponent();
         _dataSet = CreateDataSet();
         dataGridView.DataSource = _dataSet.Tables["Books"];
      }

      protected override void OnFormClosing(FormClosingEventArgs e)
      {
         _dataSet.Dispose();
      }

      private static DataSet CreateDataSet()
      {         
         using (var conn = new SqlConnection(BooksConStr))
         {
            conn.Open();
            using (var cmd = new SqlCommand(SELECT_CMD, conn))
            using (var adapter = new SqlDataAdapter())
            {
               // Объекты-адаптеры умеют взаимодействовать с конкретными серверами баз данных
               adapter.TableMappings.Add("Table", "Books");
               adapter.SelectCommand = cmd;
               var dataSet = new DataSet("Books");
               // Поместить все строки в набор данных
               adapter.Fill(dataSet);
               return dataSet;
            }
         }
      }
   }
}
