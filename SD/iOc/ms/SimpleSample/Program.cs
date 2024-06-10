using System;
using DepLib;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleSample
{
   internal static class Program
   {
      private static IServiceProvider Container { get; set; }

      private static void Main()
      {
         RegisterServices();
         WithoutUsingContainer();
         UsingContainer();
      }

      private static void RegisterServices()
      {
         var services = new ServiceCollection();
         services.AddSingleton<IGreetingService, GreetingService>();
         services.AddTransient<HelloController>();
         Container = services.BuildServiceProvider();
      }

      private static void WithoutUsingContainer()
      {
         IGreetingService service = new GreetingService();
         var controller = new HelloController(service);
         var greeting = controller.Action("Matthias");
         Console.WriteLine(greeting);
      }

      private static void UsingContainer()
      {
         var controller = Container.GetService<HelloController>();
         var greeting = controller.Action("Stephanie");
         Console.WriteLine(greeting);
      }
   }
}