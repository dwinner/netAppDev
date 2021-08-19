/**
 * Ограничение типа и фабрики.
 */

namespace GenericFactory
{
   class Program
   {
      static void Main()
      {
      }
   }

   class Factory<T> where T : new()
   {
      public T Create()
      {
         return new T();
      }
   }

   interface IFactory<out T> where T : new ()
   {
      T Create();
   }

   class Generator<T, TFactory>
      where T : new ()
      where TFactory : IFactory<T>, new()
   {
      public T CreateNew()
      {
         var factory = new TFactory();
         return factory.Create();
      }
   }
}
