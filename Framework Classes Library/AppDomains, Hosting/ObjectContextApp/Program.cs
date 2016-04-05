using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace ObjectContextApp
{
   class Program
   {
      static void Main(string[] args)
      {
         SportsCar sport = new SportsCar();
         Console.WriteLine();

         SportsCar sport2 = new SportsCar();
         Console.WriteLine();

         SportsCarThreadSafety syncSport = new SportsCarThreadSafety();

         Console.ReadKey(true);
      }
   }

   /// <summary>
   /// SportsCar не имеет никаких специальных контекстных потребностей и будет
   /// загружаться в создаваемый по умолчанию контекст внутри домена приложения.
   /// </summary>
   class SportsCar
   {
      public SportsCar()
      {
         // Информация о контексте и вывод его идентификатора.
         Context context = Thread.CurrentContext;
         Console.WriteLine("{0} object in context {1}",
            this,
            context.ContextID);
         foreach (IContextProperty contextProperty in context.ContextProperties)
         {
            Console.WriteLine("-> Context property: {0}", contextProperty.Name);
         }
      }
   }

   /// <summary>
   /// Тип SportsCarThreadSafety требует загрузки в синхронизированный контекст.
   /// </summary>
   [Synchronization]
   class SportsCarThreadSafety : ContextBoundObject
   {
      public SportsCarThreadSafety()
      {
         Context context = Thread.CurrentContext;
         Console.WriteLine("{0} object in context {1}",
            this,
            context.ContextID);
         foreach (IContextProperty contextProperty in context.ContextProperties)
         {
            Console.WriteLine("-> Context property: {0}", contextProperty.Name);
         }
      }
   }
}
