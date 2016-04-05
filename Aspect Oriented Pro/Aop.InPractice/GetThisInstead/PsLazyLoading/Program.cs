// "Сквозная" функциональность для отложенной загрузки

using PsLazyLoading.Aspects;

namespace PsLazyLoading
{
   internal class Program
   {
      [LazyLoadGetter]
      private static SlowConstructor MyProperty => new SlowConstructor(new MyService());

      [LazyLoadStructureMap]
      private static SlowConstructor _myField;

      private static void Main(string[] args)
      {
         MyProperty.DoSomething();
         MyProperty.DoSomething();
         _myField.DoSomething();
         _myField.DoSomething();
      }
   }
}