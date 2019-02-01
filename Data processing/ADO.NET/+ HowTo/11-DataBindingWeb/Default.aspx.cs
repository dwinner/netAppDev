using System;
using System.Web.Configuration;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace DataBindingWeb
{
   public partial class Default : Page
   {
      private DataSet _dataSet;

      protected void Page_Load(object sender, EventArgs e)
      {
         _dataSet = CreateDataSet();
         BooksGridView.DataSource = _dataSet.Tables["Books"];
         BooksGridView.DataBind();
      }

      private DataSet CreateDataSet()
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
               adapter.Fill(dataSet);   // Поместить все строки в DataSet
               return dataSet;
            }
         }
      }

      public string BooksConnStr
      {
         get
         {
            return WebConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;
         }
      }
   }
}
