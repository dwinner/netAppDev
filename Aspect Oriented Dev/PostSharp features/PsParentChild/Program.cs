// АОП-композиция между объектами

using System;
using System.Collections.Generic;
using PostSharp.Patterns.Collections;
using PostSharp.Patterns.Model;

namespace PsParentChild
{
   internal static class Program
   {
      private static void Main()
      {
         var invoice = new Invoice
         {
            Lines = new List<InvoiceLine>
            {
               new InvoiceLine {Amount = 1M, Product = new Product {Name = "Product 1"}},
               new InvoiceLine {Amount = 2M, Product = new Product {Name = "Product 2"}},
               new InvoiceLine {Amount = 3M, Product = new Product {Name = "Product 3"}},
               new InvoiceLine {Amount = 4M, Product = new Product {Name = "Product 4"}},
               new InvoiceLine {Amount = 5M, Product = new Product {Name = "Product 5"}}
            }
         };
         var aggregatable = invoice.QueryInterface<IAggregatable>();
         aggregatable.VisitChildren((child, info, state) =>
         {
            var invoiceLines = child as IList<InvoiceLine>;
            if (invoiceLines != null)
            {
               foreach (var invoiceLine in invoiceLines)
               {
                  Console.WriteLine(invoiceLine);
               }
            }

            return true;
         });
      }
   }

   [Aggregatable]
   //[Disposable]
   public class Invoice
   {
      public Invoice()
      {
         Lines = new AdvisableCollection<InvoiceLine>();
      }

      [Reference]
      public Customer Customer { get; set; }

      [Child]
      public IList<InvoiceLine> Lines { get; set; }

      [Child]
      public Address DeliveryAddress { get; set; }
   }

   [Aggregatable]
   public class InvoiceLine
   {
      [Reference]
      public Product Product { get; set; }

      public decimal Amount { get; set; }

      public override string ToString()
      {
         return $"Product: {Product}, Amount: {Amount}";
      }
   }

   [Aggregatable]
   public class Address
   {
   }

   public class Customer
   {
   }

   public class Product
   {
      public string Name { get; set; }

      public override string ToString()
      {
         return $"Name: {Name}";
      }
   }
}