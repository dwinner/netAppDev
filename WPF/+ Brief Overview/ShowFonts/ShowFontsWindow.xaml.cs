using System.Windows.Media;

namespace ShowFonts
{
   public partial class ShowFontsWindow
   {
      public ShowFontsWindow()
      {
         InitializeComponent();
         DataContext = Fonts.SystemFontFamilies;
      }
   }
}