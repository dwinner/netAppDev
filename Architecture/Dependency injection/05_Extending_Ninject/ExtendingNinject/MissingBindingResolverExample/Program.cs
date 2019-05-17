using Ninject;
using Ninject.Planning.Bindings.Resolvers;

namespace MissingBindingResolverExample
{
   internal static class Program
   {
      private static void Main()
      {
         var kernel = new StandardKernel();
         kernel.Components.Add<IMissingBindingResolver, DefaultImplementationBindingResolver>();
         kernel.Get<IWriter>().Write();
      }
   }
}