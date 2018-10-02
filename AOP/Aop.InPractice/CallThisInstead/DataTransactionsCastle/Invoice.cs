using System;
using System.Collections.Generic;

namespace DataTransactionsCastle
{
   public class Invoice
   {
      public Guid Id { get; set; }
      public DateTime Date { get; set; }
      public List<string> Items { get; set; }            
   }
}