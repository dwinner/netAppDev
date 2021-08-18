using System.Windows.Media;

namespace ShapesDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         MouthPath.Data = Geometry.Parse("M 40,92 Q 57,75 80,92");
      }
   }
}