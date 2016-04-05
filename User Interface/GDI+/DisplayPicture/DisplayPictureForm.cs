using System.Drawing;
using System.Windows.Forms;

namespace DisplayPicture
{
   public partial class DisplayPictureForm : Form
   {
      private const string ImageName = "Petersburg.jpg";
      private readonly Point[] _piccyBounds;
      private readonly Image _piccyImage;

      public DisplayPictureForm()
      {
         InitializeComponent();

         _piccyImage = Image.FromFile(ImageName);
         AutoScrollMinSize = _piccyImage.Size;
         _piccyBounds = new[]
         {
            new Point(0, 0), // верхний левый
            new Point(_piccyImage.Width, 0), // верхний правый
            new Point(0, _piccyImage.Height) // нижний левый
         };
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         var dc = e.Graphics;
         dc.ScaleTransform(1.0f, 1.0f);
         dc.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
         dc.DrawImage(_piccyImage, _piccyBounds);
      }
   }
}