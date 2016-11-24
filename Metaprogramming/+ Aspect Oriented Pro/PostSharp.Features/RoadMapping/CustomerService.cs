using System;
using System.Collections.Generic;
using MethodBoundarySample.Aspects;

namespace MethodBoundarySample
{
   public sealed class CustomerService
   {
      private static string _name;

      public static void MyStaticMethod()
      {
         _name = "Bob";
      }

      [LogEventAspect]
      public event EventHandler CustomerSuccessfullySaved;

      //[ConsoleWriteLineAspect]
      public void Save(Customer aCustomer)
      {
         Console.WriteLine("_name - {0}", aCustomer.Name ?? "No name");
         Console.WriteLine("Address - {0}", aCustomer.Address ?? "No address");
         OnCustomerSuccessfullySaved();
      }

      //[ConsoleWriteLineAspect]
      [ApplicationExceptionHandlerAspect]
      public IEnumerable<Customer> FetchAll()
      {
         throw new ApplicationException("We failed to fetch");
      }

      private void OnCustomerSuccessfullySaved()
      {
         CustomerSuccessfullySaved?.Invoke(this, EventArgs.Empty);
      }
   }
}