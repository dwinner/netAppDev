namespace HostView
{
   public abstract class ImageProcessorHostView
   {
      public abstract byte[] ProcessImageBytes(byte[] pixels);

      public abstract void Initialize(HostObject host);
   }
}