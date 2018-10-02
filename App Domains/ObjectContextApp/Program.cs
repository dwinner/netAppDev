using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace ObjectContextApp
{
   internal static class Program
   {
      private static void Main()
      {
         var sport = new SportsCar();
         Console.WriteLine(sport);

         var sport2 = new SportsCar();
         Console.WriteLine(sport2);

         var syncSport = new SportsCarThreadSafety();
         Console.WriteLine(syncSport);

         Console.ReadKey(true);
      }
   }

   /// <summary>
   ///    SportsCar не имеет никаких специальных контекстных потребностей и будет
   ///    загружаться в создаваемый по умолчанию контекст внутри домена приложения.
   /// </summary>
   internal class SportsCar
   {
      public SportsCar()
      {
         // Информация о контексте и вывод его идентификатора.
         var context = Thread.CurrentContext;
         Console.WriteLine("{0} object in context {1}",
            this,
            context.ContextID);
         foreach (var contextProperty in context.ContextProperties)
            Console.WriteLine("-> Context property: {0}", contextProperty.Name);
      }
   }

   /// <summary>
   ///    Тип SportsCarThreadSafety требует загрузки в синхронизированный контекст.
   /// </summary>
   [Synchronization]
   internal class SportsCarThreadSafety : ContextBoundObject
   {
      public SportsCarThreadSafety()
      {
         var context = Thread.CurrentContext;
         Console.WriteLine("{0} object in context {1}",
            this,
            context.ContextID);
         foreach (var contextProperty in context.ContextProperties)
            Console.WriteLine("-> Context property: {0}", contextProperty.Name);
      }
   }
}