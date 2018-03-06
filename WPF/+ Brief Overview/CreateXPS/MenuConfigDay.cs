using System;

namespace Wrox.ProCSharp.Documents
{
   [Serializable]
   public class MenuConfigDay
   {
      public DayOfWeek DayOfWeek { get; set; }
      public decimal Price { get; set; }
   }
}