using System;
using System.Windows.Forms;
using System.Threading;

namespace LocWinForms
{
   public partial class MainForm : Form
   {
      public MainForm()
      {         
         Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

         InitializeComponent();

         textBoxName.Text = "Joe";
         const int number = 100001;         
         textBoxNumber.Text = number.ToString("N0");
         textBoxBirthDate.Text = new DateTime(1979, 1, 15).ToLongDateString();
         labelMessage.Text = Properties.Resources.Message;
      }
   }
}
