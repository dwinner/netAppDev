using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomControl
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void OnValuesChanged(object sender, EventArgs e)
      {
         var r = redControl.Value;
         var g = greenControl.Value;
         var b = blueControl.Value;

         colorControl.BackColor = Color.FromArgb(r, g, b);
      }
   }
}