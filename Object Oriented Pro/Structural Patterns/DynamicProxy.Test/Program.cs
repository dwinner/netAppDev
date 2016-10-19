using DynamicProxy;
using System;

namespace TestConsole
{
   internal static class DynamicDecoratorTest
   {
      private static void Main()
      {
         IEmployee employee = new Employee(1, "John", "Smith", new DateTime(1990, 4, 1));

         var tpCheckRight = ObjectProxyFactory.CreateProxy(
            employee, new[] { "Salary" }, new Decoration(UserRightCheck), null);
         var tpLogCheckRight = ObjectProxyFactory.CreateProxy(
            tpCheckRight, new[] { "Salary", "FullName" }, new Decoration(EnterLog), new Decoration(ExitLog));

         // Используем отражение для декорации всех методов: var ifaceMethods = typeof (IEmployee).GetMethods().Select(info => info.Name);

         try
         {
            tpLogCheckRight.FullName();
            Console.WriteLine();
            tpLogCheckRight.Salary();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         Console.Read();
      }

      private static void UserRightCheck(object target, object[] parameters)
      {
         Console.WriteLine("Do security check here");
      }

      private static void ExitLog(object target, object[] parameters)
      {
         Console.WriteLine("Do exit log here");
      }

      private static void EnterLog(object target, object[] parameters)
      {
         Console.WriteLine("Do enter log here");
      }
   }
}