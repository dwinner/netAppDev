using System.Collections.Generic;

namespace FactoryMethod
{
   public interface IPage<T>
      where T : IModule
   {
      IList<T> PageCompositor { get; }

      void AddModule();

      void DisplayPage();
   }
}