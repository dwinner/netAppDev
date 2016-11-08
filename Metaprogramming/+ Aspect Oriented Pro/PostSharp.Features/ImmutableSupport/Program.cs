// Поддержка неизменяемости объектов со стороны аспектов

using System;
using PostSharp.Patterns.Collections;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Threading;

namespace ImmutableSupport
{
   internal static class Program
   {
      private static void Main()
      {
         try
         {
            var invoice = new Invoice(1);
            invoice.Items.Add(new Item("wrong operation"));
         }
         catch (ObjectReadOnlyException objectReadOnlyEx)
         {
            Console.WriteLine(objectReadOnlyEx.Message);
         }
      }
   }

   [Immutable]
   public class Item
   {
      public Item(string name)
      {
         Name = name;
      }

      public string Name { get; set; }
   }

   [Immutable]
   public class Document
   {
      private long _id;

      protected Document(long id)
      {
         _id = id;
      }
   }

   [Immutable]
   public class Invoice : Document
   {
      public Invoice(long id) : base(id)
      {
         Items = new AdvisableCollection<Item> { new Item("widget") };
      }

      [Child]
      public AdvisableCollection<Item> Items { get; }
   }
}