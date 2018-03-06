using System;
using System.Windows.Forms;

namespace DataGridViewDataDesigner
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }

      private void MainForm_Load(object sender, EventArgs e)
      {
         // TODO: данная строка кода позволяет загрузить данные в таблицу "inventoryDataSet.Inventory". При необходимости она может быть перемещена или удалена.
         this.inventoryTableAdapter.Fill(this.inventoryDataSet.Inventory);
      }

      private void inventoryBindingSource_CurrentChanged(object sender, EventArgs e)
      {

      }

      private void updateInventoryButton_Click(object sender, EventArgs e) // Обновление таблицы Inventory
      {
         try
         {
            inventoryTableAdapter.Update(inventoryDataSet.Inventory); // Отправка всех изменений
            inventoryTableAdapter.Fill(inventoryDataSet.Inventory); // Получение свежей копии для графической таблицы}
         }
         catch (Exception exception)
         {
            MessageBox.Show(exception.Message);
         }
      }
   }
}
