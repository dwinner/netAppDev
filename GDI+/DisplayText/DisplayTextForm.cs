using System.Drawing;
using System.Windows.Forms;

namespace DisplayText
{
   public partial class DisplayTextForm : Form
   {
      private readonly Brush _blackBrush = Brushes.Black;
      private readonly Brush _blueBrush = Brushes.Blue;
      private readonly Font _boldTimesFont = new Font("Times New Roman", 10, FontStyle.Bold);
      private readonly Font _haettenschweilerFont = new Font("Haettenschweiler", 12);
      private readonly Font _italicCourierFont = new Font("Courier", 11, FontStyle.Italic | FontStyle.Underline);

      public DisplayTextForm()
      {
         InitializeComponent();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         var dc = e.Graphics;
         dc.DrawString("This is a groovy string", _haettenschweilerFont, _blackBrush, 10, 10);
         dc.DrawString("This is a groovy string with some very long text that will never fit in the box", _boldTimesFont,
            _blueBrush, new Rectangle(new Point(10, 40), new Size(100, 40)));
         dc.DrawString("This is a groovy string", _italicCourierFont, _blackBrush, new Point(10, 100));
      }
   }
}