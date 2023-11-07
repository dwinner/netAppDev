// Внедрение в метод динамическим proxy

using System;
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace DataTransactionsCastle
{
   internal class Program
   {
      private static void Main()
      {
         var proxyGenerator = new ProxyGenerator();
         var invoiceService = proxyGenerator.CreateClassProxy<InvoiceService>(new TransationWithRetries(3));

         var invoice = new Invoice
         {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Items = new List<string> { "Item1", "Item2", "Item3" }
         };

         invoiceService.Save(invoice);
         //invoiceService.SaveRetry(invoice);
         //invoiceService.SaveFail(invoice);
         Console.WriteLine("Saved successfull");
      }
   }
}