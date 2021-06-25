using System;
using System.Windows.Forms;
using AutoLotEntityLibrary;
using EdmWinFormBinding.Properties;

namespace EdmWinFormBinding
{
   public partial class InventoryForm : Form
   {
      private readonly AutoLotEntities _autoLotEntities = new AutoLotEntities();

      public InventoryForm()
      {
         InitializeComponent();         
      }

      private void InvForm_Load(object sender, EventArgs e) // Привязка данных к сетке
      {         
         inventoryDataGridView.DataSource = _autoLotEntities.Inventories;         
      }     

      private void InvForm_Closed(object sender, FormClosedEventArgs e) // Убиваем сущностный объект
      {
         _autoLotEntities.Dispose();
      }

      private void updateButton_Click(object sender, EventArgs e)
      {
         _autoLotEntities.SaveChanges();
         MessageBox.Show(Resources.updateButtonClickDataSavedRes);
      }          
   }
}
