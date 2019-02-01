using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UpdateDatabaseFromDataSet
{
   public partial class MainForm : Form
   {
      private const string UPDATE_BOOKS_CMD =
         "UPDATE BOOKS SET Title = @Title, PublishYear = @PublishYear WHERE BookID = @BookID";

      private const string SELECT_CMD = "SELECT * FROM Books";
      private IDataAdapter _adapter;
      private DataSet _dataSet;
      private IDbConnection _conn;

      private static readonly string BooksConnStr = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;

      public MainForm()
      {
         InitializeComponent();
         buttonUpdate.Enabled = false;
         CreateDataSet();
         dataGridView.DataSource = _dataSet.Tables["Books"];
         dataGridView.CellEndEdit += (o, e) => buttonUpdate.Enabled = true;
         buttonUpdate.Click += (o, e) =>
         {
            _adapter.Update(_dataSet);
            buttonUpdate.Enabled = false;
         };         
      }

      protected override void OnClosing(CancelEventArgs e)
      {
         _dataSet.Dispose();
         _conn.Dispose();
      }

      private void CreateDataSet()
      {
         var conn = new SqlConnection(BooksConnStr);
         var selectCmd = new SqlCommand(SELECT_CMD, conn);
         var updatecmd = new SqlCommand(UPDATE_BOOKS_CMD, conn);

         // Необходимы параметры, чтобы объект DataSet мог
         // подставлять корректные значения для столбцов
         updatecmd.Parameters.AddRange(new SqlParameter[]
         {
            new SqlParameter("@BookID", SqlDbType.Int, 4, "BookID"),
            new SqlParameter("@Title", SqlDbType.VarChar, 255, "Title"),
            new SqlParameter("@PublishYear", SqlDbType.Int, 4, "PublishYear")
         });

         var adapter = new SqlDataAdapter();

         adapter.TableMappings.Add("Table", "Books");
         adapter.SelectCommand = selectCmd;
         adapter.UpdateCommand = updatecmd;
         _conn = conn;
         _adapter = adapter;
         _dataSet = new DataSet("Books");

         // Поместить все строки в набор данных
         _adapter.Fill(_dataSet);
      }
   }
}
