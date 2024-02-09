using Plugin.Common;

namespace CapitalizerPlugin
{
   public class CapitalizerPlugin : ITextPlugin
   {
      public string TransformText(string input) => input.ToUpper();
   }
}