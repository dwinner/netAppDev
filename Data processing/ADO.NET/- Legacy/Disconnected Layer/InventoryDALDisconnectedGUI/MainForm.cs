using System;
using System.Data;
using System.Windows.Forms;
using AutoLotDAL;

namespace InventoryDALDisconnectedGUI
{
   public partial class MainForm : Form
   {
      readonly InventoryDalDisLayer _dal;

      public MainForm()
      {
         InitializeComponent();
         const string connectionString = "Data Source=Hi-Tech-PC;Initial Catalog=AutoLot;Integrated Security=True;Pooling=False";         
         _dal = new InventoryDalDisLayer(connectionString);         
         inventoryGrid.DataSource = _dal.GetAllInventory();
      }

      #region Update click logic
      private void btnUpdateInventory_Click(object sender, EventArgs e)
      {         
         DataTable changedDt = (DataTable) inventoryGrid.DataSource;
         try
         {            
            _dal.UpdateInventory(changedDt);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      #endregion
   }
}
