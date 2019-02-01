using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ListFonts
{
   public partial class EnumFontsForm : Form
   {
      private const int TextMargin = 10;
      private static readonly Font[] InstalledFonts;

      static EnumFontsForm()
      {
         var fontCollection = new InstalledFontCollection();
         var fontFamilies = fontCollection.Families;
         InstalledFonts = new Font[fontFamilies.Length];
         for (var i = 0; i < InstalledFonts.Length; i++)
         {
            if (fontFamilies[i].IsStyleAvailable(FontStyle.Regular))
            {
               InstalledFonts[i] = new Font(fontFamilies[i].Name, 10);
            }
         }
      }

      public EnumFontsForm()
      {
         InitializeComponent();
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         var verticalCoordinate = TextMargin;
         e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

         foreach (var installedFont in InstalledFonts)
         {
            var point = new Point(TextMargin, verticalCoordinate);
            verticalCoordinate += installedFont.Height;
            e.Graphics.DrawString(installedFont.Name, installedFont, Brushes.Black, point);
         }
      }
   }
}