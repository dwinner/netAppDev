using System;
using System.Windows.Forms;

namespace FlickerFree
{
   public partial class DoubleBufferedForm : Form
   {
      public DoubleBufferedForm()
      {
         InitializeComponent();
      }

      private void checkBoxFlickerFree_CheckedChanged(object sender, EventArgs e)
      {
         drawPanel.DoubleBuffered = !drawPanel.DoubleBuffered;
      }
   }
}