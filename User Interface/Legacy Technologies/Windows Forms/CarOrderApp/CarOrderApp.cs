using System;
using System.Windows.Forms;
using CarOrderApp.Properties;

namespace CarOrderApp
{
   public partial class CarOrderApp : Form
   {
      public CarOrderApp()
      {
         InitializeComponent();
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Close();
      }

      private void orderAutoToolStripMenuItem_Click(object sender, EventArgs e)
      {
         OrderAutoDialog orderAuto = new ImageOrderAutoDialog();
         if (orderAuto.ShowDialog() == DialogResult.OK)
         {
            var make = orderAuto.MakeTextBox.Text;
            var text = orderAuto.ColorTextBox.Text;
            var price = orderAuto.PriceTextBox.Text;
            var orderInfo = string.Format("Mark: {0}, Color: {1}, Price: {2}", make, text, price);
            MessageBox.Show(orderInfo, Resources.OrderInfo);
         }
      }
   }
}