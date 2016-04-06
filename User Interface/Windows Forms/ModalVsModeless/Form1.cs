using System;
using System.Windows.Forms;

namespace ModalVsModeless
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void buttonCreateModal_Click(object sender, EventArgs e)
      {
         // Открыть модальное окно
         var form = new PopupForm();
         form.ShowDialog(this);
      }

      private void buttonCreateModeless_Click(object sender, EventArgs e)
      {
         // Открыть немодальное окно
         var form = new PopupForm();
         // Создание родительского окна позволит сделать поведение родителя
         // и потомка более эффективным, особенно при работе с активными окнами
         // и сворачивании окон
         form.Show(this);
      }
   }
}