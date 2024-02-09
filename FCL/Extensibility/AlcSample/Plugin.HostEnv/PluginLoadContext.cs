using System.Reflection;
using System.Runtime.Loader;
using Plugin.Common;

namespace Plugin.HostEnv;

internal class PluginLoadContext(string pluginPath, bool collectible)
   : AssemblyLoadContext(Path.GetFileName(pluginPath), collectible)
{
   private readonly AssemblyDependencyResolver _resolver = new(pluginPath);

   // Create a resolver to help us find dependencies.

   protected override Assembly Load(AssemblyName assemblyName)
   {
      // See below
      if (assemblyName.Name == typeof(ITextPlugin).Assembly.GetName().Name)
      {
         return null!;
      }

      var target = _resolver.ResolveAssemblyToPath(assemblyName);
      if (target != null)
      {
         return LoadFromAssemblyPath(target);
      }

      // Could be a framework assembly. Allow the default context to resolve.
      return null!;
   }

   protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
   {
      var path = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
      return path == null
         ? IntPtr.Zero
         : LoadUnmanagedDllFromPath(path);
   }
}