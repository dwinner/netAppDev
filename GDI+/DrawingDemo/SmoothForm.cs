using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DrawingDemo
{
   public partial class SmoothForm : Form
   {
      public SmoothForm()
      {
         InitializeComponent();

         radioButtonSmoothingAntiAlias.Tag = SmoothingMode.AntiAlias;
         radioButtonSmoothingDefault.Tag = SmoothingMode.Default;
         radioButtonSmoothingHighQuality.Tag = SmoothingMode.HighQuality;
         radioButtonSmoothingHighSpeed.Tag = SmoothingMode.HighSpeed;
         radioButtonSmoothingNone.Tag = SmoothingMode.None;

         radioButtonTextSmoothingAntiAlias.Tag = TextRenderingHint.AntiAlias;
         radioButtonTextSmoothingAntiAliasGridFit.Tag = TextRenderingHint.AntiAliasGridFit;
         radioButtonTextSmoothingClearTypeGridFit.Tag = TextRenderingHint.ClearTypeGridFit;
         radioButtonTextSmoothingDefault.Tag = TextRenderingHint.SystemDefault;
         radioButtonTextSmoothingSingleBitPerPixel.Tag = TextRenderingHint.SingleBitPerPixel;
         radioButtonTextSmoothingSingleBitPerPixelGridFit.Tag = TextRenderingHint.SingleBitPerPixelGridFit;
      }

      private void OnSmoothingChanged(object sender, EventArgs e)
      {
         var button = sender as RadioButton;
         if (button != null)
         {
            drawingPanel.SmoothingMode = (SmoothingMode)button.Tag;
         }
      }

      private void button1_Click(object sender, EventArgs e)
      {
         var fd = new FontDialog { Font = drawingPanel.TextFont };
         if (fd.ShowDialog() == DialogResult.OK)
         {
            drawingPanel.TextFont = fd.Font;
         }
      }

      private void numericUpDown1_ValueChanged(object sender, EventArgs e)
      {
         drawingPanel.TextAngle = (int)numericUpDown1.Value;
      }

      private void OnTextRenderingChanged(object sender, EventArgs e)
      {
         var button = sender as RadioButton;
         if (button != null)
         {
            drawingPanel.TextRenderingHint = (TextRenderingHint)button.Tag;
         }
      }
   }
}