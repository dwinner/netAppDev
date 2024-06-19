namespace VirtualProxy;

internal class Bitmap : IImage
{
   private readonly string _filename;

   public Bitmap(string filename)
   {
      _filename = filename;
      Console.WriteLine($"Loading image from {filename}");
   }

   public void Draw()
   {
      Console.WriteLine($"Drawing image {_filename}");
   }
}