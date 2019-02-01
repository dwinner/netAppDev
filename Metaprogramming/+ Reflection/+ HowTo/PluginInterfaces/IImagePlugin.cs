namespace PluginInterfaces
{
   public interface IImagePlugin
   {
      System.Drawing.Image RunPlugin(System.Drawing.Image image);

      string Name { get; }
   }
}
