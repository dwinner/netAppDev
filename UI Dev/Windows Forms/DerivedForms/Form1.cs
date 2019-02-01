using System;
using System.Windows.Forms;

namespace DerivedForms
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void buttonShowBaseForm_Click(object sender, EventArgs e)
      {
         var form = new BaseForm();
         form.Show();
      }

      private void buttonShowInheritedForm_Click(object sender, EventArgs e)
      {
         var form = new InheritedForm();
         form.Show();
      }
   }
}