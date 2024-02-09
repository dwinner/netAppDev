using System.Reflection;
using Plugin.Common;

namespace Plugin.HostEnv;

internal static class Program
{
   private const bool UseCollectibleContexts = true;

   private static void Main()
   {
      var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      var captializer = Path.Combine(location!, "plg", "CapitalizerPlugin.dll");
      Console.WriteLine(captializer);
      Console.WriteLine(TransformText("big apple", captializer));
   }

   private static string TransformText(string text, string pluginPath)
   {
      var alc = new PluginLoadContext(pluginPath, UseCollectibleContexts);
      try
      {
         var assem = alc.LoadFromAssemblyPath(pluginPath);

         // Locate the type in the assembly that implements ITextPlugin:
         var pluginType = assem.ExportedTypes.Single(type => typeof(ITextPlugin).IsAssignableFrom(type));

         // Instantiate the ITextPlugin implementation:
         var plugin = (ITextPlugin)Activator.CreateInstance(pluginType)!;

         // Call the TransformText method
         return plugin.TransformText(text);
      }
      finally
      {
         if (UseCollectibleContexts)
         {
            alc.Unload(); // unload the ALC
         }
      }
   }
}