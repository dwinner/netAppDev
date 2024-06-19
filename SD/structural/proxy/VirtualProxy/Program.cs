using static System.Console;

namespace VirtualProxy;

internal static class Program
{
   private static void Main()
   {
      var img = new LazyBitmap("pokemon.png");
      DrawImage(img);
   }

   private static void DrawImage(IImage img)
   {
      WriteLine("About to draw the image");
      img.Draw();
      WriteLine("Done drawing the image");
   }
}