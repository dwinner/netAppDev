using Ninject;
using Ninject.Extensions.Conventions;
using Player.Core;
using static System.Console;

namespace Player.Console
{
   internal static class Program
   {
      private static void Main()
      {
         // Plugin approach
         using (var stdKernel = new StandardKernel())
         {
            stdKernel.Bind(syntax =>
               syntax.FromAssembliesMatching("*").SelectAllClasses().InheritedFrom<ICodec>().BindAllInterfaces());
            foreach (var codec in stdKernel.GetAll<ICodec>())
            {
               WriteLine($"Codec: {codec.Name}");
            }
         }

         // Manual approach
         /*
         using (var kernel = new StandardKernel(new InternalCodecsModule()))
         {
            var codecs = kernel.GetAll<ICodec>();
            foreach (var codec in codecs)
            {
               System.Console.WriteLine(codec.Name);
            }

            kernel.Load("plugins/*.dll");
            var player = kernel.Get<Core.Player>();
            player.Play(new FileInfo(@"c:\music.mp3"));
         }
         */
      }
   }
}