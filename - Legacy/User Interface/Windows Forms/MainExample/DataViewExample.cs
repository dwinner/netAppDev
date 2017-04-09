using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MainExample
{
   public partial class DataViewExample : Form
   {
      public DataViewExample()
      {
         InitializeComponent();
      }

      private void getData_Click(object sender, EventArgs e)
      {
         const string productsSelect = "SELECT * FROM products";

         using (
            var northSqlConnection =
               new SqlConnection(ConfigurationManager.ConnectionStrings["northwind"].ConnectionString))
         {
            var northDataAdapter = new SqlDataAdapter(productsSelect, northSqlConnection);
            var northSet = new DataSet();
            northDataAdapter.Fill(northSet, "Products");
            originalData.AutoGenerateColumns = true;
            originalData.DataSource = northSet.Tables["Products"];
            var northDataView = new DataView(northSet.Tables["Products"]);
            filteredData.AutoGenerateColumns = true;
            filteredData.DataSource = northDataView;
            comboBox1.SelectedIndex = 6;
            comboBox1.Enabled = true;
         }
      }

      private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
      {
         DataViewRowState state;

         switch (comboBox1.Text)
         {
            case "Added":
               state = DataViewRowState.Added;
               break;
            case "CurrentRows":
               state = DataViewRowState.CurrentRows;
               break;
            case "Deleted":
               state = DataViewRowState.Deleted;
               break;
            case "ModifiedCurrent":
               state = DataViewRowState.ModifiedCurrent;
               break;
            case "ModifiedOriginal":
               state = DataViewRowState.ModifiedOriginal;
               break;
            case "None":
               state = DataViewRowState.None;
               break;
            case "OriginalRows":
               state = DataViewRowState.OriginalRows;
               break;
            case "Unchanged":
               state = DataViewRowState.Unchanged;
               break;
            default:
               state = DataViewRowState.OriginalRows;
               break;
         }

         try
         {
            ((DataView)filteredData.DataSource).RowStateFilter = state;
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.ToString());
         }
      }
   }
}