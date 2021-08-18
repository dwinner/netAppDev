/**
 * NotNull attribute injector via rewriting IL
 */

using System.IO;
using static NotNullAttribute.Lib.InjectorExtensions;

namespace NotNullParameterInjector.Sandbox
{
   internal static class EntryPoint
   {
      private static void Main()
      {
         var dllToInject = $"{nameof(NotNullParameterInjector)}.{nameof(InjectMe)}.dll";
         var assemblyLocation = new FileInfo(dllToInject);
         InjectAssembly(assemblyLocation);
      }
   }
}