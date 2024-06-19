namespace VirtualProxy;

internal class LazyBitmap(string filename) : IImage
{
   private Bitmap? _bitmap;

   public void Draw()
   {
      _bitmap ??= new Bitmap(filename);
      _bitmap.Draw();
   }
}