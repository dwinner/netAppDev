//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryEdmConsoleApp
{
   using System;
   using System.Collections.Generic;

   public partial class Car
   {
      public int CarId { get; set; }
      public string Make { get; set; }
      public string Color { get; set; }
      public string CarNickname { get; set; }

      public override string ToString()
      {
         return string.Format("{0} is a {1} {2} with id {3}",
                              CarNickname ?? "No name",
                              Color,
                              Make,
                              CarId);
      }
   }
}
