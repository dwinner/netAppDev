using System;
using System.Windows.Forms;

namespace MainExample
{
   public partial class DataGridViewExample : Form
   {
      private readonly Action<DataGridView> _populate;

      public DataGridViewExample()
      {
         InitializeComponent();
      }

      public DataGridViewExample(Action<DataGridView> populate)
      {
         InitializeComponent();
         _populate = populate;
      }

      private void getDataButton_Click(object sender, EventArgs e)
      {
         _populate(dataGridView1);
      }
   }
}