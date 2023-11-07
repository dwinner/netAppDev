using System.Diagnostics;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NotNullAttribute.Lib;

namespace NotNullParameterInjector.InjectMe
{
   public sealed class InjectorTask : AppDomainIsolatedTask
   {
      [Required]
      public string AssemblyLocation { get; set; }

      public override bool Execute()
      {
         Log.LogMessage("Injecting assembly {0}...", AssemblyLocation);
         var stopwatch = Stopwatch.StartNew();
         InjectorExtensions.InjectAssembly(new FileInfo(AssemblyLocation));
         stopwatch.Stop();
         Log.LogMessage("Assembly injection for {0} complete - total time: {1}.",
            AssemblyLocation,
            stopwatch.Elapsed.ToString());

         return true;
      }
   }
}