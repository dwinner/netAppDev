using System;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace HostApp2
{
   public static class ContainerConfigurationExtensions
   {
      public static ContainerConfiguration WithAssembliesInPath(this ContainerConfiguration @this,
         string path,
         SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
         WithAssembliesInPath(@this, path, null, searchOption);

      public static ContainerConfiguration WithAssembliesInPath(this ContainerConfiguration @this,
         string path,
         AttributedModelProvider conventions,
         SearchOption searchOption = SearchOption.TopDirectoryOnly)
      {
         try
         {
            var assemblies =
               (from dll in Directory.GetFiles(path, "*.dll", searchOption)
                  let context = new AssemblyLoadContext(null)
                  select context.LoadFromAssemblyPath(dll))
               .ToList();

            @this = @this.WithAssemblies(assemblies, conventions);
         }
         catch (Exception ex)
         {
            Debug.WriteLine(ex.Message);
         }

         return @this;
      }
   }
}