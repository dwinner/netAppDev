using System;
using System.Windows.Forms;
using ListenToEvents.Properties;

namespace ListenToEvents
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();

         // Добавить собственный метод в список делегатов
         // события Click для кнопки
         BigButton.Click += BigButton_Click;
         // Отписаться от события: BigButton.Click -= BigButton_Click;
      }

      static void BigButton_Click(object sender, EventArgs e)
      {
         MessageBox.Show(Resources.MainForm_BigButton_Click_This_is_too_easy);
      }
   }
}
